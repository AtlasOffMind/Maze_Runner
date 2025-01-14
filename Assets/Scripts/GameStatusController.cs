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
        private List<Player> posibleTargets;


        public TextMeshProUGUI _textLifePoints;
        public TextMeshProUGUI _textStepLeft;
        public TextMeshProUGUI _textTurn;
        public TextMeshProUGUI _textCoolDown;
        public TextMeshProUGUI _textAmount;
        public TextMeshProUGUI _textbutton1;
        public TextMeshProUGUI _textbutton2;
        public TextMeshProUGUI _textbutton3;

        public GameObject _abilitySelectionPanel;
        public Button button1;
        public Button button2;
        public Button button3;

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

            if(posibleTargets == null)
            posibleTargets = TMtemp.GetPlayersInGame();

            

            if (_textLifePoints != null && _textStepLeft != null && _textTurn != null && _textCoolDown != null)
            {
                _textLifePoints.text = "Life Points: " + players.lifePoints;
                _textStepLeft.text = "Steps left: " + players.steps;
                _textTurn.text = "Turn: " + players.gameObject.name;
                _textCoolDown.text = "CoolDown: Turns left " + players.CoolDown;

                if (players.GetComponent<AbilityHolder>().ability.name != "Disarmer")
                {
                    _textAmount.gameObject.SetActive(false);
                }
                else
                {
                    _textAmount.text = "Amount: " + players.amount;
                    _textAmount.gameObject.SetActive(true);
                }

            }

            if (players.GetComponent<AbilityHolder>().ability.name == "CopyCat" && players.GetComponent<AbilityHolder>().ability.isOn)
            {
                posibleTargets.Remove(players);
                _abilitySelectionPanel.SetActive(true);
                _textbutton1.text = posibleTargets[0].gameObject.name;
                _textbutton2.text = posibleTargets[1].gameObject.name;
                _textbutton3.text = posibleTargets[2].gameObject.name;
            }
            else { _abilitySelectionPanel.SetActive(false); }
        }


    }
}