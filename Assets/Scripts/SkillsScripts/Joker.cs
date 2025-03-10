using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

[CreateAssetMenu]
public class Joker : Ability
{
    int _ExtraLife;
    List<Player> playersList;
    TurnManagement TM;
    public override void Activate(Player parent)
    {
        Player player = parent;
        if (player.lifePoints < 5)
        {
            TM = FindFirstObjectByType<TurnManagement>();
            playersList = TM.GetPlayersInGame();
            _ExtraLife = 0;

            for (int i = 0; i < playersList.Count; i++)
                _ExtraLife += playersList[i].lifePoints;

            player.lifePoints += _ExtraLife;

            player.CoolDown = CoolDown;
        
        }
    }
}
