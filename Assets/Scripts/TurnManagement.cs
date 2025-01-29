using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace MazeRunner
{
    public class TurnManagement : MonoBehaviour
    {
        private Player playerTurn;
        private List<Player> playersInGame;
        //private List<Player> tempPlayersInGame;
        private int listIndexer = 0;
        private MazeGenerator mazeGenerator;
        private int originalSteps = 0;
        private Motion M;
        public IEnumerator currentRoutine;

        public LayerMask detectionLayer;
        public LayerMask wallLayer;
        public LayerMask playerLayer;

        public void PlayerSelect(List<Player> playerselect)
        {
            mazeGenerator = FindFirstObjectByType<MazeGenerator>();

            if (playersInGame == null)
            {
                playersInGame = playerselect;
                //tempPlayersInGame = playerselect;

                for (int i = 0; i < playersInGame.Count; i++)
                {
                    Destroy(playersInGame[i].GetComponent<Motion>());
                    playersInGame[i].GetComponent<AbilityHolder>().enabled = false;
                }

            }

            playerTurn = playersInGame[listIndexer];

            playerTurn.GetComponent<AbilityHolder>().enabled = true;

            if (playerTurn.CoolDown != 0) playerTurn.CoolDown--;

            playerTurn.gameObject.AddComponent<Motion>();

            M = FindFirstObjectByType<Motion>();
            M.detectionLayer = detectionLayer;
            M.wallLayer = wallLayer;
            M.playerLayer = playerLayer;

            originalSteps = playerTurn.steps;
            playerTurn.inTurn = true;
        }

        public Player GetPlayer() => playerTurn;
        public List<Player> GetPlayersInGame() => playersInGame;

        public bool ContainsPlayer(Vector3 targetPosition)
        {
            for (int i = 0; i < playersInGame.Count; i++)
            {
                if (targetPosition.x == playersInGame[i].transform.position.x && targetPosition.z == playersInGame[i].transform.position.z) return true;
            }
            return false;
        }

        public Player GetPlayer(Vector3 targetPosition)
        {
            for (int i = 0; i < playersInGame.Count; i++)
            {
                if (targetPosition.x == playersInGame[i].transform.position.x && targetPosition.z == playersInGame[i].transform.position.z) return playersInGame[i];
            }
            return null;
        }

        public void EndTurn()
        {

            //if (playersInGame.Count != tempPlayersInGame.Count) { playersInGame = mazeGenerator.GetPlayers(); }

            if (playerTurn.penaltyTurn != 0) playerTurn.penaltyTurn--;

            if (playersInGame.Count == 0) return;

            playerTurn.steps = originalSteps;
            playerTurn.inTurn = false; //Finalizar el Turno del jugador actual
            Destroy(playerTurn.GetComponent<Motion>());

            playerTurn.GetComponent<AbilityHolder>().enabled = false;

            playerTurn = null;
            originalSteps = 0;

            //Cambiar al siguiente jugador4
            listIndexer = (listIndexer + 1) % playersInGame.Count;

            PlayerSelect(playersInGame);
        }

    }
}