using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeRunner;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class CreditsMenu : MonoBehaviour
{
    public List<Player> players;
    public float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo
    private Player winDancer;
    private string winCharacterName;
    public GameObject panel;


    void Start()
    {
        winCharacterName = WinCharacter.instance.nameOfWinner;

        for (int i = 0; i < players.Count; i++)
        {

            if (players[i].name == winCharacterName && !players[i].gameObject.activeInHierarchy)
                winDancer = players[i];
        }
        winDancer.gameObject.SetActive(true);


        Invoke("WaitToEnd", 30);
        Invoke("SeeDancer", 15);
    }
    void Update()
    {
        winDancer.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void WaitToEnd()
    {
        SceneManager.LoadScene(0);
    }

private void SeeDancer()
{
    panel.SetActive(false);
}
}
