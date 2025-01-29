using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;


[CreateAssetMenu]
public class Disarmer : Ability
{
    private int amount; // Número de trampas a desarmar.
    private Player player;
    private Motion motion;
    private MazeCell currentCell;
    List<MazeCell> tempCell;


    public override void Activate(Player parent)
    {
        player = parent;
        motion = player.GetComponent<Motion>();
        amount = 10; // Número de trampas que puede desarmar.
        player.amount = amount;
        List<MazeCell> tempCell = new List<MazeCell>();
         player.CoolDown = CoolDown; 

    }

    public override void Fast()
    {
        if (player == null || motion == null) return;

        currentCell = motion.GetMazeCell(player.transform.position);

        if ((currentCell._VioletHole.CompareTag("VioletTrap") || currentCell._GreenSpike.CompareTag("GreenTrap") || currentCell._InvisibleTrap.CompareTag("InvisibleTrap")) && !tempCell.Contains(currentCell))
        {
            currentCell._GreenSpike.SetActive(false); currentCell._GreenSpike.tag = "Untagged";
            currentCell._InvisibleTrap.SetActive(false); currentCell._InvisibleTrap.tag = "Untagged";
            currentCell._VioletHole.SetActive(false); currentCell._VioletHole.tag = "Untagged";
            currentCell._NormalFloor.SetActive(true);

            player.amount--;          
        }
    }
}
