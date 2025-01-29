using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public List<GameObject> panels;
    int indexer = 0;

    public void NextPanel()
    {
        panels[indexer].SetActive(false);
        indexer = (indexer + 1) % panels.Count;
        panels[indexer].SetActive(true);
    }
    public void PreviousPanel()
    {
        panels[indexer].SetActive(false);
        indexer--;
        if (indexer < 0)
        {
            indexer = panels.Count - 1;
        }
        panels[indexer].SetActive(true);
    }


}
