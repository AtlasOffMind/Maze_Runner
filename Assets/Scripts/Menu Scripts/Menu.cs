using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject _OptionMenu;
    public GameObject _TutorialMenu;
    float posFinal;
    bool abrirMenu = true;
    public float tiempo = 0.5f;
    void Start()
    {
        posFinal = Screen.width / 2;
    }
    IEnumerator Mover(float time, Vector3 posInit, Vector3 posFin, GameObject currentMenu)
    {
        GameObject menu = currentMenu;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            menu.transform.position = Vector3.Lerp(posInit, posFin, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        menu.transform.position = posFin;


    }

    void MoverMenu(float time, Vector3 posInit, Vector3 posFin, GameObject currentMenu)
    {
        StartCoroutine(Mover(time, posInit, posFin,currentMenu));
    }


    public void ButtonOptionMenu()
    {
        int signo = 1;
        if (!abrirMenu)
            signo = -1;

        MoverMenu(tiempo, _OptionMenu.transform.position, new Vector3(signo * posFinal, _OptionMenu.transform.position.y, 0), _OptionMenu);

        abrirMenu = !abrirMenu;
    }
    public void ButtonTutorialMenu()
    {
        int signo = 1;
        if (!abrirMenu)
            signo = -1;

        MoverMenu(tiempo, _TutorialMenu.transform.position, new Vector3(signo * posFinal, _TutorialMenu.transform.position.y, 0), _TutorialMenu);

        abrirMenu = !abrirMenu;
    }



    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
