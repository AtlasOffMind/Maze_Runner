using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MazeRunner
{


    public class GameStatusController : MonoBehaviour
    {
        Player players;

        public TextMeshProUGUI _textLifePoints;
        public TextMeshProUGUI _textStepLeft;
        public TextMeshProUGUI _textTurn;
        public TextMeshProUGUI _textCoolDown;

        public static GameStatusController Gamestatuscontroller;
        private void Awake()
        {
            //players.AddRange(FindObjectsByType());
            //players = new List<Player>();

            Gamestatuscontroller = this;
        }


        void Update()
        {
            if(players == null)
            players = FindAnyObjectByType<Player>();

            if (_textLifePoints != null && _textStepLeft != null && _textTurn != null)
            {
                _textLifePoints.text = "Life Points: " + players.lifePionts;
                _textStepLeft.text = "Steps left: " + players.steps;

            }

        }
    }
}