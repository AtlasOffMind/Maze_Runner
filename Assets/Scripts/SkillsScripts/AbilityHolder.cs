using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private Ability savedAbility;
    int CoolDown = 0;
    public KeyCode key;
    TurnManagement TM;
    Player player;

    void Update()
    {
        if (TM == null)
        {
            TM = FindAnyObjectByType<TurnManagement>();
            savedAbility = ability;
            ability.SetOn(false);
        }

        player = TM.GetPlayer();

        if (player.CoolDown == 0 && player.amount == 0 && player.specialSteps == 0 && player.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(key))
            {
                ability.Activate(player);
                CoolDown = ability.CoolDown;
            }
        }

        if (player.amount != 0 && ability.name == "Disarmer") { ability.Fast(); }

        if (savedAbility.name == "CopyCat" && player.CoolDown != 0) { ability = savedAbility; }

        if (ability.isOn && ability.name == "Intangible") { ability.Fast(); }

        if (ability.isOn && ability.name == "Control")
        {
            if (player.steps >= 5) player.steps -= 5;
            ability.Fast();
        }

    }
    public void SaveAbility(Ability first, Ability second)
    {
        savedAbility = first;
        ability = second;
        ability.Activate(player);
    }
}
