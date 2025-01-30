using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MazeRunner
{

    public class MazeGenerator : MonoBehaviour
    {
        // Prefab de la celda del laberinto, usado para instanciar las celdas en el juego.
        [SerializeField]
        private MazeCell _MazeCellPrefab;

        [SerializeField]
        public Player[] _PlayerPrefab = new Player[5];

        private List<Player> _PlayerListPrefab;

        // Filas del laberinto en número de celdas.
        [SerializeField]
        private int _MazeWidth;

        // Columnas del laberinto en número de celdas.
        [SerializeField]
        private int _MazeDepth;

        [SerializeField]
        public int numberOfPlayers;

        // Matriz que representa la estructura del laberinto.
        [SerializeField]
        private MazeCell[,] _MazeGrid;
        private List<MazeCell> entrance = new List<MazeCell>();
        private List<MazeCell> exit = new List<MazeCell>();
        private TurnManagement TM;

        // Método Start que se ejecuta al iniciar el juego.
        void Start()
        {
            // Inicializa la matriz que representará la cuadrícula del laberinto.
            _MazeGrid = new MazeCell[_MazeWidth, _MazeDepth];
            _PlayerListPrefab = new List<Player>();
            TM = FindFirstObjectByType<TurnManagement>();


            // Bucle doble para instanciar cada celda en la cuadrícula del laberinto.
            for (int x = 0; x < _MazeWidth; x++)
            {
                for (int z = 0; z < _MazeDepth; z++)
                {
                    // Instancia una nueva celda del laberinto en la posición (x, z).
                    _MazeGrid[x, z] = Instantiate(_MazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    _MazeGrid[x, z].gameObject.name = "MazeCell [" + x + "," + z + "]";
                }
            }

            // Llama al método para generar el laberinto comenzando desde la celda en la esquina superior izquierda.
            GenerateMaze(null, _MazeGrid[0, 0]);

            SetEntranceAndExit();

            Traps.Iniciate();

            PuttingTramps();

            if (numberOfPlayers >= 1 && numberOfPlayers <= 4) PlacingPlayer();
            else if (numberOfPlayers < 1) { numberOfPlayers = 1; PlacingPlayer(); }
            else { numberOfPlayers = 4; PlacingPlayer(); }

            TM.PlayerSelect(GetPlayers());

            for (int i = 0; i < exit.Count; i++)
            {
                exit[i]._MazeCellTrigger.SetActive(true);
                exit[i]._MazeCellTrigger.GetComponent<Ontriggered>().enabled = true;
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
            List<BoxCollider> colliders = new List<BoxCollider>();

            // Esto es una lista que guarda las posiciones de las entradas. 
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
                if (i < 2)
                    entrance[i].ChangeColor(Color.blue); // Marca las entradas en Azul.
                else entrance[i].ChangeColor(Color.red);

                //Aqui les estoy poniendo a las etiquetas para identificar las celdas de ENTRADA/SALIDA. 
                entrance[i].tag = "Entrance";
                exit[i].tag = "Exit";
                exit[i].gameObject.name = "Salida [" + (int)exit[i].transform.position.x + "," + (int)exit[i].transform.position.z + "]";

                exit[i].ChangeColor(Color.black);// Marca la salida en negro.
                exit[i]._NormalFloor.SetActive(true);// puse esto xq me estaba dando un error donde no se generaba el piso.

                //Llamadas a los metodos Clear para eliminar las paredes del centro de la salida.
                exit[i].ClearBackWall();
                exit[i].ClearRigthtWall();
                exit[i].ClearFronttWall();
                exit[i].ClearLeftWall();

                colliders.Add(exit[i].gameObject.GetComponentInChildren<BoxCollider>());
                colliders[i].isTrigger = true;
            }

        }

        private void PuttingTramps()
        {
            //Este for esta exclusivamente creado para poner aleatoriamente las trampas en el juego.
            for (int x = 2; x < _MazeWidth - 2; x++)
            {
                for (int z = 2; z < _MazeDepth - 2; z++)
                {
                    if (_MazeGrid[x, z].CompareTag("Exit")) continue;

                    int TrapNum = Random.Range(0, 100); //
                    MazeCell temp = _MazeGrid[x, z];

                    if (TrapNum > 20 && TrapNum < 25)
                    {
                        temp._NormalFloor.SetActive(false);
                        temp._GreenSpike.SetActive(true);

                        temp._GreenSpike.tag = "GreenTrap";
                        Traps.Save(temp);
                    }
                    else if (TrapNum > 40 && TrapNum < 45)
                    {
                        temp._NormalFloor.SetActive(false);
                        temp._VioletHole.SetActive(true);

                        temp._VioletHole.tag = "VioletTrap";
                        Traps.Save(temp);
                    }
                    else if (TrapNum > 60 && TrapNum < 65)
                    {
                        temp._NormalFloor.SetActive(false);
                        temp._InvisibleTrap.SetActive(true);

                        temp._InvisibleTrap.tag = "InvisibleTrap";
                        Traps.Save(temp);
                    }
                }
            }
        }

        private void PlacingPlayer()
        {

            List<string> ListTeamBlue = new List<string>();
            List<string> ListTeamRed = new List<string>();


            ListTeamBlue.Add(CharacterKeepping.instance.blueTeam1);
            ListTeamBlue.Add(CharacterKeepping.instance.blueTeam2);
            ListTeamRed.Add(CharacterKeepping.instance.redTeam1);
            ListTeamRed.Add(CharacterKeepping.instance.redTeam2);


            List<Vector3> entrancePositions = new List<Vector3>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                entrancePositions.Add(entrance[i].transform.position);
            }

            int index = 0;
            foreach (Player character in _PlayerPrefab)
            {
                // Si el personaje está en la lista de seleccionados
                if (ListTeamBlue.Contains(character.gameObject.transform.GetChild(0).name))
                {
                    // Activar el personaje
                    character.gameObject.SetActive(true);

                    character.Team = "Blue";

                    // Colocarlo en la posición correspondiente
                    if (index < _PlayerPrefab.Length)
                    {
                        character.transform.SetPositionAndRotation(new Vector3(entrancePositions[index].x, character.transform.position.y, entrancePositions[index].z), character.transform.rotation);
                        _PlayerListPrefab.Add(character);
                        index++;
                    }

                }
                else
                {
                    // Si el personaje NO está en la lista de seleccionados, desactivarlo
                    character.gameObject.SetActive(false);
                    character.GetComponent<AbilityHolder>().enabled = false;
                }
            }

            foreach (Player character in _PlayerPrefab)
            {
                // Si el personaje está en la lista de seleccionados
                if (ListTeamRed.Contains(character.gameObject.transform.GetChild(0).name))
                {
                    // Activar el personaje
                    character.gameObject.SetActive(true);

                    character.Team = "Red";

                    // Colocarlo en la posición correspondiente
                    if (index < _PlayerPrefab.Length)
                    {
                        character.transform.SetPositionAndRotation(new Vector3(entrancePositions[index].x, character.transform.position.y, entrancePositions[index].z), character.transform.rotation);
                        _PlayerListPrefab.Add(character);
                        index++;
                    }

                }
                else if (!ListTeamRed.Contains(character.gameObject.transform.GetChild(0).name) && !_PlayerListPrefab.Contains(character))
                {
                    // Si el personaje NO está en la lista de seleccionados, desactivarlo
                    character.gameObject.SetActive(false);
                    character.GetComponent<AbilityHolder>().enabled = false;
                }
            }
        }

        public List<MazeCell> GetEntrance() => entrance;
        public List<MazeCell> GetExit() => exit;
        public MazeCell[,] GetMatrix() => _MazeGrid;
        public List<Player> GetPlayers() => _PlayerListPrefab;


        public bool NearExit(Vector3 targetPosition)
        {
            for (int x = (_MazeWidth / 2) - 3; x < (_MazeWidth / 2) + 3; x++)
            {
                for (int z = (_MazeDepth / 2) - 3; z < (_MazeDepth / 2) + 3; z++)
                {
                    if (targetPosition.x == _MazeGrid[x, z].transform.position.x && targetPosition.z == _MazeGrid[x, z].transform.position.z) return true;
                }
            }
            return false;
        }

        private void SetRandomEntrance()
        {//Esto es lo q habia escrito para generar aleatoriamente las entradas.
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

            int random1 = Random.Range(0, 29);
            int random2 = Random.Range(0, 29);
            int random3 = Random.Range(0, 29);
            int random4 = Random.Range(0, 29);

            entrance.Add(entrance1[random1]);
            entrance.Add(entrance2[random2]);
            entrance.Add(entrance3[random3]);
            entrance.Add(entrance4[random4]);
        }
    }
}


/*  */


