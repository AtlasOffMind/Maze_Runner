using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using MazeRunner;


public class Ontriggered : MonoBehaviour
{
    TurnManagement turnManagement;
    Player Winplayer;

    MazeGenerator mazeGenerator;

    MazeCell[,] mazeGrid;
    List<Player> playersInGame;

    void Start()
    {
        mazeGenerator = FindFirstObjectByType<MazeGenerator>();
        mazeGrid = mazeGenerator.GetMatrix();
        playersInGame = mazeGenerator.GetPlayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        Winplayer = PlayerWithMotionActive();

        SaveData();

        SceneManager.LoadScene(3);
    }

    Player PlayerWithMotionActive()
    {
        for (int i = 0; i < playersInGame.Count; i++)
            if (playersInGame[i].GetComponent<Motion>() != null)
                return playersInGame[i];

        return null;

    }

    private void SaveData()
    {
        WinCharacter.instance.nameOfWinner = Winplayer.gameObject.transform.GetChild(0).name;
    }
}
