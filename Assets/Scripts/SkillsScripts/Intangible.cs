using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;


[CreateAssetMenu]
public class Intangible : Ability
{
    private Player player;
    private int specialSteps;
    public override void Activate(Player parent)
    {
        player = parent;
        if (player.steps > 10 && player.CoolDown == 0)
        {
            specialSteps = 10;
            player.specialSteps = specialSteps;
            SetOn(true);
            player.CoolDown = CoolDown; // Aplica el enfriamiento.
        }
        else
        {
            Debug.Log("You can't use this skill with less than 10 steps left");
        }
    }
    public override void Fast()
    {
        if (player.specialSteps <= 0)
        {
            SetOn(false);
            Debug.Log(player.GetComponent<AbilityHolder>().ability.isOn);
            player.specialSteps = 0;
        }
    }


}
