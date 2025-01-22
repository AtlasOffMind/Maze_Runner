using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

[CreateAssetMenu]
public class Joker : Ability
{
    Player player;
    // Start is called before the first frame update
    public override void Activate(Player parent)
    {
        player = parent;
        if (player.steps > 10 && player.CoolDown == 0)
        {
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
            player.specialSteps = 0;
        }
    }
}
