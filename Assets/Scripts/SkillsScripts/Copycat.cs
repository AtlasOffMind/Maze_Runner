using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeRunner;
using UnityEngine.UI;

[CreateAssetMenu]
public class Copycat : Ability
{
    private Ability copiedAbility;
    private List<Player> posibleTargets;
    private Player temp;
    private List<int> tempList;
    private TurnManagement TM;
    private static GameStatusController GSC;
    int index;

    public override void Activate(Player parent)
    {
        posibleTargets = new List<Player>();
        tempList = new List<int>();
        TM = FindFirstObjectByType<TurnManagement>();
        GSC = FindFirstObjectByType<GameStatusController>();
        copiedAbility = null;
        temp = parent;

        posibleTargets = TM.GetPlayersInGame();

        for(int i = 0; i < posibleTargets.Count; i++)
        {
            if(posibleTargets[i] != parent )
            {
                tempList.Add(i);
            }
        }

        SetOn(true);

        GSC.button1.onClick.AddListener(SelectFirstOption);
        GSC.button2.onClick.AddListener(SelectSecondOption);
        GSC.button3.onClick.AddListener(SelectThirdOption);
    }
    private void SelectFirstOption()
    {
        copiedAbility = posibleTargets[0].GetComponent<AbilityHolder>().ability;
        Debug.Log("Ahora CopiedAbility es " + copiedAbility.name);

        SetOn(false);
        temp.GetComponent<AbilityHolder>().SaveAbility(temp.GetComponent<AbilityHolder>().ability, copiedAbility);

        for (int i = 0; i < posibleTargets.Count; i++) Debug.Log($"CopyCat: [{i}] = {posibleTargets[i].name}");

    }
    private void SelectSecondOption()
    {
        copiedAbility = posibleTargets[1].GetComponent<AbilityHolder>().ability;
        Debug.Log("Ahora CopiedAbility es " + copiedAbility.name);
        temp.GetComponent<AbilityHolder>().SaveAbility(temp.GetComponent<AbilityHolder>().ability, copiedAbility);

    }
    private void SelectThirdOption()
    {
        copiedAbility = posibleTargets[2].GetComponent<AbilityHolder>().ability;
        Debug.Log("Ahora CopiedAbility = " + copiedAbility.name);
        temp.GetComponent<AbilityHolder>().SaveAbility(temp.GetComponent<AbilityHolder>().ability, copiedAbility);
    }

}

