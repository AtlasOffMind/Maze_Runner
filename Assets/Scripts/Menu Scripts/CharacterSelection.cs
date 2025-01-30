using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using MazeRunner;


public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> blueTeam;
    public List<GameObject> redTeam;

    public List<GameObject> visualBlueTeamCube0;
    public List<GameObject> visualBlueTeamCube1;
    public List<GameObject> visualRedTeamCube0;
    public List<GameObject> visualRedTeamCube1;

    int firstSelection = 1;
    int characterIndex = 0;
    bool theGameisReady = false;

    public GameObject descriptionPanel;

    public TextMeshProUGUI _skillNameText;
    public TextMeshProUGUI _skillDescriptionText;

    private GameObject currentCharacter;


    public GameObject playButton;
    public GameObject readyText;



    void Start()
    {
        blueTeam = new List<GameObject>();
        redTeam = new List<GameObject>();
        currentCharacter = characters[characterIndex];
    }

    public void NextCaracter()
    {

        characters[characterIndex].SetActive(false);
        characterIndex = (characterIndex + 1) % characters.Count;
        characters[characterIndex].SetActive(true);

        currentCharacter = characters[characterIndex];

    }
    public void PreviousCaracter()
    {
        characters[characterIndex].SetActive(false);
        characterIndex--;
        if (characterIndex < 0)
        {
            characterIndex = characters.Count - 1;
        }
        characters[characterIndex].SetActive(true);

        currentCharacter = characters[characterIndex];

    }

    public void IsReadyToPlay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }


    void Update()
    {
        if (currentCharacter != null)
            _skillNameText.text = currentCharacter.GetComponent<AbilityHolder>().ability.name;

        if (currentCharacter.GetComponent<AbilityHolder>().ability.name == "Control") _skillDescriptionText.text = "This character has the power of change places with another token on your command, if the other token position isn't very near to the exit";
        else if (currentCharacter.GetComponent<AbilityHolder>().ability.name == "Disarmer") _skillDescriptionText.text = "This character can remove a limited number of traps and not being affected by them";
        else if (currentCharacter.GetComponent<AbilityHolder>().ability.name == "Intangible") _skillDescriptionText.text = "This character has special steps , limited , that allows him to pass throw walls, but the traps has effect on him ";
        else if (currentCharacter.GetComponent<AbilityHolder>().ability.name == "Joker") _skillDescriptionText.text = "This character heals itself with the Life Point of the others token even his own, only if he has less than 5 HP";
        else if (currentCharacter.GetComponent<AbilityHolder>().ability.name == "Long Walk") _skillDescriptionText.text = "This character add 25 steps points at his own ";



        if (Input.GetKeyDown(KeyCode.Return) && firstSelection == 1 && !readyText.activeInHierarchy)
        {
            characters[characterIndex].SetActive(false);

            blueTeam.Add(characters[characterIndex]);
            int temp = characterIndex;

            characterIndex = 0;

            characters.RemoveAt(temp);

            characters[characterIndex].SetActive(true);

            if (blueTeam.Count == 2)
            {
                readyText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && firstSelection == 2 && !readyText.activeInHierarchy)
        {
            characters[characterIndex].SetActive(false);

            redTeam.Add(characters[characterIndex]);
            int temp = characterIndex;

            characterIndex = 0;

            characters.RemoveAt(temp);
            characters[characterIndex].SetActive(true);

            if (redTeam.Count == 2)
            {
                theGameisReady = true;

                firstSelection = 3;
            }
        }

        if (theGameisReady)
        {
            playButton.SetActive(true);
        }

        if (readyText.activeInHierarchy && Input.GetKeyDown(KeyCode.Y))
        {
            readyText.SetActive(false);
            firstSelection += 1;
        }


        if (blueTeam.Count == 1)
        {
            for (int i = 0; i < visualBlueTeamCube0.Count; i++)
            {
                if (visualBlueTeamCube0[i].name == blueTeam[0].name)
                    visualBlueTeamCube0[i].SetActive(true);
            }
        }
        else if (blueTeam.Count == 2)
        {
            for (int i = 0; i < visualBlueTeamCube1.Count; i++)
            {
                if (visualBlueTeamCube1[i].name == blueTeam[1].name)
                    visualBlueTeamCube1[i].SetActive(true);
            }
        }

        if (redTeam.Count == 1)
        {
            for (int i = 0; i < visualRedTeamCube0.Count; i++)
            {
                if (visualRedTeamCube0[i].name == redTeam[0].name)
                    visualRedTeamCube0[i].SetActive(true);
            }
        }
        else if (redTeam.Count == 2)
        {
            for (int i = 0; i < visualRedTeamCube1.Count; i++)
            {
                if (visualRedTeamCube1[i].name == redTeam[1].name)
                    visualRedTeamCube1[i].SetActive(true);
            }
        }

    }

    private void SaveData()
    {
        CharacterKeepping.instance.blueTeam1 = blueTeam[0].name;
        CharacterKeepping.instance.blueTeam2 = blueTeam[1].name;
        CharacterKeepping.instance.redTeam1 = redTeam[0].name;
        CharacterKeepping.instance.redTeam2 = redTeam[1].name;
    }

}
