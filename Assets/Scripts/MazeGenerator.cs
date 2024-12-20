using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Prefab de la celda del laberinto, usado para instanciar las celdas en el juego.
    [SerializeField]
    private MazeCell _MazeCellPrefab;

    // Ancho del laberinto en número de celdas.
    [SerializeField]
    private int _MazeWidth;

    // Profundidad del laberinto en número de celdas.
    [SerializeField]
    private int _MazeDepth;

    // Matriz que representa la estructura del laberinto.
    [SerializeField]
    private MazeCell[,] _MazeGrid;



    // Método Start que se ejecuta al iniciar el juego.
    void Start()
    {
        // Inicializa la matriz que representará la cuadrícula del laberinto.
        _MazeGrid = new MazeCell[_MazeWidth, _MazeDepth];

        // Bucle doble para instanciar cada celda en la cuadrícula del laberinto.
        for (int x = 0; x < _MazeWidth; x++)
        {
            for (int z = 0; z < _MazeDepth; z++)
            {
                // Instancia una nueva celda del laberinto en la posición (x, z).
                _MazeGrid[x, z] = Instantiate(_MazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Llama al método para generar el laberinto comenzando desde la celda en la esquina superior izquierda.
        GenerateMaze(null, _MazeGrid[0, 0]);

        //Este for esta exclusivamente creado para poner aleatoriamente las trampas en el juego.
        for (int x = 2; x < _MazeWidth - 2; x++)
        {
            for (int z = 2; z < _MazeDepth - 2; z++)
            {
                int TrapNum = Random.Range(0, 100); //
                MazeCell temp = _MazeGrid[x, z];

                if (TrapNum > 20 && TrapNum < 25)
                {
                    temp._NormalFloor.SetActive(false);
                    temp._GreenSpike.SetActive(true);
                    temp.HasATrap= true; 
                }
                else if (TrapNum > 40 && TrapNum < 45)
                {
                    temp._NormalFloor.SetActive(false);
                    temp._VioletHole.SetActive(true); 
                    temp.HasATrap= true; 
                }
                else if (TrapNum > 60 && TrapNum < 65)
                {
                    temp._NormalFloor.SetActive(false);
                    temp._InvisibleTrap.SetActive(true);
                    temp.HasATrap= true; 
                }

                //Aqui les estoy(pretendo) poniendo a las etiquetas para identificar las celdas de TRAMPAS. *****************************
                //if (temp._InvisibleTrap == true)
                //_MazeGrid[x,z].tag = "Trap";
            }
        }


        SetEntranceAndExit();

        if (!IsPathValid())
            Debug.LogError("No hay un camino válido entre la entrada y la salida.");
        else
        {
            Debug.LogWarning("Forzando conexión entre entrada y salida.");
            ForceConnection();
        }
    }

    // Método recursivo para generar el laberinto.
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        // Marca la celda actual como visitada.
        currentCell.Visit();

        // Limpia las paredes entre la celda actual y la anterior (si existe).
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        // Bucle que busca celdas no visitadas y llama recursivamente para continuar la generación.
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    // Obtiene la siguiente celda no visitada adyacente a la celda actual, seleccionada aleatoriamente.
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();// Le da un numero aleatorio a las celdas dadas por GetUnvisitedCells y se queda con el primero.
    }

    // Devuelve un IEnumerable de celdas no visitadas alrededor de la celda actual.
    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        // Comprueba la celda a la derecha.
        if (x + 1 < _MazeWidth)
        {
            var cellToRight = _MazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            { yield return cellToRight; }//devuelve la posicion de la celda si no ha sido visitada.
        }

        // Comprueba la celda a la izquierda.
        if (x - 1 >= 0)
        {
            var cellToLeft = _MazeGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
            { yield return cellToLeft; }
        }

        // Comprueba la celda hacia el frente.
        if (z + 1 < _MazeDepth)
        {
            var cellToFront = _MazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            { yield return cellToFront; }
        }

        // Comprueba la celda hacia atrás.
        if (z - 1 >= 0)
        {
            var cellToBack = _MazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            { yield return cellToBack; }
        }
    }

    // Limpia las paredes entre dos celdas adyacentes.
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
            return;

        // Determina la posición relativa entre las celdas para limpiar las paredes correspondientes.
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRigthtWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRigthtWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFronttWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFronttWall();
            return;
        }
    }

    private void SetEntranceAndExit()
    {
        // Esto es una lista que guarda las posiciones de las entradas. 
        List<MazeCell> entrance = new List<MazeCell>();
        entrance.Add(_MazeGrid[0, 0]);
        entrance.Add(_MazeGrid[_MazeWidth - 1, 0]);
        entrance.Add(_MazeGrid[0, _MazeDepth - 1]);
        entrance.Add(_MazeGrid[_MazeWidth - 1, _MazeDepth - 1]);

        // Esto es una lista que guarda las posiciones de las salidas. 
        List<MazeCell> exit = new List<MazeCell>();
        exit.Add(_MazeGrid[(_MazeWidth - 1) / 2, (_MazeDepth - 1) / 2]);
        exit.Add(_MazeGrid[((_MazeWidth - 1) / 2) + 1, (_MazeDepth - 1) / 2]);
        exit.Add(_MazeGrid[(_MazeWidth - 1) / 2, ((_MazeDepth - 1) / 2) + 1]);
        exit.Add(_MazeGrid[((_MazeWidth - 1) / 2) + 1, ((_MazeDepth - 1) / 2) + 1]);

        // Cambia la apariencia visual de la entrada y salida
        for (int i = 0; i < 4; i++)
        {
            entrance[i].ChangeColor(Color.grey); // Marca las entradas en gris.

            //Aqui les estoy(pretendo) poniendo a las etiquetas para identificar las celdas de ENTRADA/SALIDA. 
            entrance[i].tag = "Entrance";
            exit[i].tag = "Exit";

            exit[i].ChangeColor(Color.black);// Marca la salida en negro.
            exit[i]._GreenSpike.SetActive(false);
            exit[i]._InvisibleTrap.SetActive(false);
            exit[i]._VioletHole.SetActive(false);
            exit[i]._NormalFloor.SetActive(true);// puse esto xq me estaba dando un error donde no se generaba el piso.

            //Llamadas a los metodos Clear para eliminar las paredes del centro de la salida.
            exit[i].ClearBackWall();
            exit[i].ClearRigthtWall();
            exit[i].ClearFronttWall();
            exit[i].ClearLeftWall();
        }
    }
    private bool IsPathValid()
    {
        var visited = new HashSet<MazeCell>();
        var queue = new Queue<MazeCell>();
        queue.Enqueue(_MazeGrid[0, 0]); // Inicia desde la celda de entrada.

        while (queue.Count > 0)
        {
            MazeCell current = queue.Dequeue();
            if (visited.Contains(current))
                continue;

            visited.Add(current);

            // Si llegamos a la salida, el camino es válido.
            if (current == _MazeGrid[_MazeWidth - 1, _MazeDepth - 1])
                return true;

            // Añade las celdas vecinas accesibles a la cola.
            foreach (MazeCell neighbor in GetAccessibleNeighbors(current))
            {
                if (!visited.Contains(neighbor))
                    queue.Enqueue(neighbor);
            }
        }

        return false; // No se encontró un camino a la salida.
    }

    private IEnumerable<MazeCell> GetAccessibleNeighbors(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;


        // Comprueba las celdas vecinas y verifica si están conectadas.
        if (x + 1 < _MazeWidth && !_MazeGrid[x, z].HasRigthWall())
            yield return _MazeGrid[x + 1, z];

        if (x - 1 >= 0 && !_MazeGrid[x, z].HasLeftWall())
            yield return _MazeGrid[x - 1, z];

        if (z + 1 < _MazeDepth && !_MazeGrid[x, z].HasFrontWall())
            yield return _MazeGrid[x, z + 1];

        if (z - 1 >= 0 && !_MazeGrid[x, z].HasBackWall())
            yield return _MazeGrid[x, z - 1];
    }

    private void ForceConnection()
    {
        MazeCell entrance = _MazeGrid[0, 0];
        MazeCell exit = _MazeGrid[_MazeWidth - 1, _MazeDepth - 1];

        // Conecta directamente la entrada y la salida eliminando las paredes entre ellas.
        entrance.ClearFronttWall();
        exit.ClearBackWall();
    }

    // Método Update vacío, podría ser eliminado si no se necesita en el futuro.
    void Update()
    {

    }
}




/*  */


