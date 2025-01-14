using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;


namespace MazeRunner
{
    public static class Traps
    {
        public static List<MazeCell> VioletTrap;
        public static List<MazeCell> InvisibleTrap;
        public static List<MazeCell> GreenTrap;
        public static List<UnityEngine.Vector3> playersInGamePos;

        private static MazeCell[,] MazeGrid;
        private static MazeGenerator mazeGenerator;
        private static TurnManagement TM;

        public static void Iniciate()
        {
            mazeGenerator = UnityEngine.MonoBehaviour.FindFirstObjectByType<MazeGenerator>();
            MazeGrid = mazeGenerator.GetMatrix();

            TM = UnityEngine.MonoBehaviour.FindFirstObjectByType<TurnManagement>();

            VioletTrap = new List<MazeCell>();
            InvisibleTrap = new List<MazeCell>();
            GreenTrap = new List<MazeCell>();
            playersInGamePos = new List<UnityEngine.Vector3>();
        }

        public static void Save(MazeCell cell)
        {
            if (cell._VioletHole.CompareTag("VioletTrap")) VioletTrap.Add(cell);

            if (cell._InvisibleTrap.CompareTag("InvisibleTrap")) InvisibleTrap.Add(cell);

            if (cell._GreenSpike.CompareTag("GreenTrap")) GreenTrap.Add(cell);
        }
        public static void TrapIsActive(Player _PlayerPrefab, MazeCell currentCell)
        {
            TrapHability(_PlayerPrefab, currentCell);
        }
        private static void TrapHability(Player _PlayerPrefab, MazeCell currentCell)
        {
            if (_PlayerPrefab.gameObject.GetComponent<AbilityHolder>().ability.name == "Disarmer" && _PlayerPrefab.amount != 0) return;

            if (currentCell._VioletHole.CompareTag("VioletTrap"))
            {
                _PlayerPrefab.penaltyTurn = 3;
            }
            if (currentCell._InvisibleTrap.CompareTag("InvisibleTrap"))
            {
                for (int i = 0; i < TM.GetPlayersInGame().Count; i++)
                { playersInGamePos.Add(TM.GetPlayersInGame()[i].transform.position); }

                playersInGamePos.Remove(_PlayerPrefab.transform.position);

                int x = Random.Range(0, 29);
                int z = Random.Range(0, 29);

                if (!InvisibleTrap.Contains(MazeGrid[x, z]) && !VioletTrap.Contains(MazeGrid[x, z]) && !GreenTrap.Contains(MazeGrid[x, z]) && !playersInGamePos.Contains(new UnityEngine.Vector3(x, _PlayerPrefab.transform.position.y, z)) && !currentCell.CompareTag("Exit"))
                {
                    _PlayerPrefab.GetComponent<Motion>().StopMovement();
                    _PlayerPrefab.transform.SetPositionAndRotation(new UnityEngine.Vector3(x, _PlayerPrefab.transform.position.y, z), _PlayerPrefab.transform.rotation);
                    if (_PlayerPrefab.steps == 0) TM.EndTurn();
                    playersInGamePos.Clear();
                }
                else
                {
                    playersInGamePos.Clear();
                    TrapHability(_PlayerPrefab, currentCell);
                }
            }
            if (currentCell._GreenSpike.CompareTag("GreenTrap"))
            {
                Debug.Log("hola");
                _PlayerPrefab.lifePoints--;
            }



        }
        public static void Print()
        {
            Debug.Log("VioletTrap " + VioletTrap[0].name);
            Debug.Log("InvisibleTrap " + InvisibleTrap[0].name);
            Debug.Log("GreenTrap " + GreenTrap[0].name);
        }



    }

}
