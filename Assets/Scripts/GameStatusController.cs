using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        public TextMeshProUGUI _textSpecialMovement;
        public TextMeshProUGUI _textbutton1;
        public TextMeshProUGUI _textbutton2;
        public TextMeshProUGUI _textbutton3;

        public GameObject _abilitySelectionPanel;
        public Button button1;
        public Button button2;
        public Button button3;

        private TurnManagement TMtemp;
        public static GameStatusController Gamestatuscontroller;

        public GameObject Panel;
        private void Start()
        {
            Gamestatuscontroller = this;
            TMtemp = FindFirstObjectByType<TurnManagement>();
            _abilitySelectionPanel.SetActive(false);
        }

        void Update()
        {
            players = TMtemp.GetPlayer();

            if (posibleTargets == null)
                posibleTargets = TMtemp.GetPlayersInGame();



            if (_textLifePoints != null && _textStepLeft != null && _textTurn != null && _textCoolDown != null)
            {
                _textLifePoints.text = "Life Points: " + players.lifePoints;
                _textStepLeft.text = "Steps left: " + players.steps;
                _textTurn.text = "Turn: " + players.Team + " Team";
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

            if (players.GetComponent<AbilityHolder>().ability.name == "Intangible" && players.GetComponent<AbilityHolder>().ability.isOn)
            {
                _textSpecialMovement.gameObject.SetActive(true);
                _textSpecialMovement.text = "Special Steps: " + players.specialSteps;
            }
            else _textSpecialMovement.gameObject.SetActive(false);


            if (players.Team == "Blue") Panel.GetComponent<Image>().color = Color.blue;
            else Panel.GetComponent<Image>().color = Color.red;

        }




    }
}