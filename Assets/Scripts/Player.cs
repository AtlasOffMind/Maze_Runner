using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeRunner
{
    public class Player : MonoBehaviour
    {
        // Atributos básicos del jugador
        public GameObject _Player;
        public int steps;
        public int lifePoints;
        public int CoolDown;
        public bool inTurn;
        public int  penaltyTurn;
        public int amount;
        public int specialSteps;
        public string Team;


        public void GettingSetting()
        {
            inTurn = false;
            if (penaltyTurn != 0) penaltyTurn = 0;
        }
    }
}
