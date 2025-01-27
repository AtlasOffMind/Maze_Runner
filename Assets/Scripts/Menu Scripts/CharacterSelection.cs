using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;


public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> blueTeam;
    public List<GameObject> redTeam;

    public GameObject playButton;
    public GameObject readyText;

    public List<GameObject> visualBlueTeamCube0;
    public List<GameObject> visualBlueTeamCube1;
    public List<GameObject> visualRedTeamCube0;
    public List<GameObject> visualRedTeamCube1;

    int firstSelection = 1;
    int characterIndex = 0;
    bool theGameisReady = false;
    int lastCount = 0;




    void Start()
    {
        blueTeam = new List<GameObject>();
        redTeam = new List<GameObject>();
    }

    public void NextCaracter()
    {

        characters[characterIndex].SetActive(false);
        characterIndex = (characterIndex + 1) % characters.Count;
        characters[characterIndex].SetActive(true);

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
    }

    public void IsReadyToPlay()
    {
        SceneManager.LoadScene(2);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }


    void Update()
    {
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

}
