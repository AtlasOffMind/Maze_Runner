using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

[CreateAssetMenu]
public class LongWalk : Ability
{
    int _ExtraSteps;

    public override void Activate(Player parent)
    {
        Player player = parent;

        _ExtraSteps = 25;

        player.steps += _ExtraSteps;

        player.CoolDown = CoolDown;
    }
}


