using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeRunner
{

    public class MazeGenerator : MonoBehaviour
    {
        // Prefab de la celda del laberinto, usado para instanciar las celdas en el juego.
        [SerializeField]
        private MazeCell _MazeCellPrefab;

        // Filas del laberinto en número de celdas.
        [SerializeField]
        private int _MazeWidth;

        // Columnas del laberinto en número de celdas.
        [SerializeField]
        private int _MazeDepth;

        // Matriz que representa la estructura del laberinto.
        [SerializeField]
        private MazeCell[,] _MazeGrid;

        public List<MazeCell> entrance = new List<MazeCell>();
        public List<MazeCell> exit = new List<MazeCell>();


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

            PuttingTramps();

            SetEntranceAndExit();

            CreatePath();

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

            /*Esto es lo q habia escrito para generar aleatoriamente las entradas.
            List<MazeCell> entrance1 = new List<MazeCell>();
            List<MazeCell> entrance2 = new List<MazeCell>();
            List<MazeCell> entrance3 = new List<MazeCell>();
            List<MazeCell> entrance4 = new List<MazeCell>();
            for (int x = 0; x < _MazeWidth - 1; x++)
            {
                entrance3.Add(_MazeGrid[x, 0]);
                entrance4.Add(_MazeGrid[x, _MazeDepth - 1]);
            }
            for (int z = 0; z < _MazeDepth - 1; z++)
            {
                entrance1.Add(_MazeGrid[0, z]);
                entrance2.Add(_MazeGrid[_MazeWidth - 1, z]);
            }
            int pointer = Random.Range(0, 0);
            entrance1[pointer].ChangeColor(Color.grey);
            entrance2[pointer].ChangeColor(Color.grey);
            entrance3[pointer].ChangeColor(Color.grey);
            entrance4[pointer].ChangeColor(Color.grey);*/

            entrance.Add(_MazeGrid[0, 0]);
            entrance.Add(_MazeGrid[_MazeWidth - 1, 0]);
            entrance.Add(_MazeGrid[0, _MazeDepth - 1]);
            entrance.Add(_MazeGrid[_MazeWidth - 1, _MazeDepth - 1]);

            // Esto es una lista que guarda las posiciones de las salidas. 
            exit.Add(_MazeGrid[(_MazeWidth - 1) / 2, (_MazeDepth - 1) / 2]);
            exit.Add(_MazeGrid[((_MazeWidth - 1) / 2) + 1, (_MazeDepth - 1) / 2]);
            exit.Add(_MazeGrid[(_MazeWidth - 1) / 2, ((_MazeDepth - 1) / 2) + 1]);
            exit.Add(_MazeGrid[((_MazeWidth - 1) / 2) + 1, ((_MazeDepth - 1) / 2) + 1]);

            // Cambia la apariencia visual de la entrada y salida
            for (int i = 0; i < 4; i++)
            {
                entrance[i].ChangeColor(Color.grey); // Marca las entradas en gris.

                //Aqui les estoy(pretendo) poniendo a las etiquetas para identificar las celdas de ENTRADA/SALIDA. 
                //entrance[i].tag = "Entrance";
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

        private void PuttingTramps()
        {
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
                        temp.HasATrap = true;
                    }
                    else if (TrapNum > 40 && TrapNum < 45)
                    {
                        temp._NormalFloor.SetActive(false);
                        temp._VioletHole.SetActive(true);
                        temp.HasATrap = true;
                    }
                    else if (TrapNum > 60 && TrapNum < 65)
                    {
                        temp._NormalFloor.SetActive(false);
                        temp._InvisibleTrap.SetActive(true);
                        temp.HasATrap = true;
                    }
                }

                //Aqui les estoy(pretendo) poniendo a las etiquetas para identificar las celdas de TRAMPAS. *****************************
                //if (temp._InvisibleTrap == true)
                //_MazeGrid[x,z].tag = "Trap";
            }
        }

        private void CreatePath()
        {
            for (int x = 0; x < _MazeWidth - 1; x++)
            {
                for (int z = 0; z < _MazeDepth - 1; z++)
                {
                    _MazeGrid[x, z].IsVisited = false;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                FindPath(entrance[i]);
            }

        }
        private bool FindPath(MazeCell currentCell)
        {
            // Marcar celda como visitada
            currentCell.Visit();

            // Comprobar si la celda actual es la salida
            if (exit.Contains(currentCell))
            {
                return true;
            }

            // Obtener celdas vecinas accesibles
            var neighbors = GetAccessibleNeighbors(currentCell);

            foreach (var neighbor in neighbors)
            {
                // Si el vecino no ha sido visitado, recursivamente intentar encontrar la salida
                if (!neighbor.IsVisited && FindPath(neighbor))
                {
                    return true;
                }
            }

            // Si ninguna opción lleva a la salida, retrocede
            return false;
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
    }
}


/*  */


