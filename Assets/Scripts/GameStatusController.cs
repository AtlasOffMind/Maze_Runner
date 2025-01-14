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
        public TextMeshProUGUI _textAmount;
        private TurnManagement TMtemp;
        public static GameStatusController Gamestatuscontroller;
        private void Start()
        {
            Gamestatuscontroller = this;
            TMtemp = FindFirstObjectByType<TurnManagement>();
        }

        void Update()
        {
            players = TMtemp.GetPlayer();
            if (_textLifePoints != null && _textStepLeft != null && _textTurn != null && _textCoolDown != null)
            {
                _textLifePoints.text = "Life Points: " + players.lifePoints;
                _textStepLeft.text = "Steps left: " + players.steps;
                _textTurn.text = "Turn: " + players.gameObject.name;
                _textCoolDown.text = "CoolDown: Turns left " + players.CoolDown;


                if (players.gameObject.GetComponent<AbilityHolder>().ability.name != "Disarmer" || players.gameObject.GetComponent<AbilityHolder>().ability == null)
                { _textAmount.gameObject.SetActive(false); }
                if (players.gameObject.GetComponent<AbilityHolder>().ability.name == "Disarmer" || players.gameObject.GetComponent<AbilityHolder>().ability == null)
                {
                    _textAmount.text = "Amount: " + players.amount;
                    _textAmount.gameObject.SetActive(true);

                }

            }

        }
    }
}