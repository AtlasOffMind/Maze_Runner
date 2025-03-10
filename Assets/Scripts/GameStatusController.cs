using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace MazeRunner
{
    /*Este script esta creado para controlar los datos mostrados en pantalla. */
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
        public TextMeshProUGUI _warning;


        public GameObject _abilitySelectionPanel;
        public Button button1;
        public Button button2;
        public Button button3;

        private TurnManagement TMtemp;
        public static GameStatusController Gamestatuscontroller;

        public GameObject Panel;
        public GameObject pauseMenu;

        public GameObject camera0;
        public GameObject camera1;
        private Vector3 cameraPos;


        private void Start()
        {
            Gamestatuscontroller = this;
            TMtemp = FindFirstObjectByType<TurnManagement>();
            _abilitySelectionPanel.SetActive(false);

        }

        void Update()
        {
            players = TMtemp.GetPlayer();

            players.transform.GetChild(1).gameObject.SetActive(true);

            if (players.Team == "Blue") players.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.blue;
            else players.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;

            if (posibleTargets == null)
                posibleTargets = TMtemp.GetPlayersInGame();

            if (players.gameObject.activeInHierarchy && players.gameObject.name == "Token (1)")
            {
                camera0.gameObject.SetActive(false);
                camera1.gameObject.SetActive(true);
                //camera2.gameObject.SetActive(true);
                //camera3.gameObject.SetActive(true);
                //camera4.gameObject.SetActive(true);
            }





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

            if (Input.GetKeyDown(KeyCode.Space) && players.GetComponent<AbilityHolder>().ability.name == "Intangible" && players.steps < 10)
            {
                _warning.transform.parent.gameObject.SetActive(true);
                _warning.text = "You can't use this skill with less than 10 steps left";
                Invoke("Off", 5f);
            }

            if (players.GetComponent<AbilityHolder>().ability.name == "Intangible" && players.GetComponent<AbilityHolder>().ability.isOn)
            {
                _textSpecialMovement.gameObject.SetActive(true);
                _textSpecialMovement.text = "Special Steps: " + players.specialSteps;
            }
            else _textSpecialMovement.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space) && players.GetComponent<AbilityHolder>().ability.name == "Joker" && players.lifePoints < 5)
            {
                _warning.transform.parent.gameObject.SetActive(true);
                _warning.text = "You can only use this skill with less than 5 HP ";
                Invoke("Off", 5f);

            }



            if (players.Team == "Blue") Panel.GetComponent<Image>().color = Color.blue;
            else Panel.GetComponent<Image>().color = Color.red;


            if (Input.GetKeyDown(KeyCode.Space) && players.CoolDown != 0)
            {
                _warning.transform.parent.gameObject.SetActive(true);
                _warning.text = "The Skill Can't be use now";
                Invoke("Off", 8f);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                players.gameObject.GetComponent<Motion>().enabled = false;
                pauseMenu.gameObject.SetActive(true);
            }

        }



        public Player GetPlayer() => players;

        public void Off()
        {
            _warning.transform.parent.gameObject.SetActive(false);
        }
    }
}