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
    }
    public override void Fast()
    {
        if (player.specialSteps <= 0)
        {
            SetOn(false);
            player.specialSteps = 0;
        }
    }


}
