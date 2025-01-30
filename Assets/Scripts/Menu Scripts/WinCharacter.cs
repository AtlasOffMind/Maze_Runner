using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCharacter : MonoBehaviour
{
    public static WinCharacter instance;

    public string nameOfWinner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
             DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
