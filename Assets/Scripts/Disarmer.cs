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
    bool isActive;

    List<MazeCell> tempCell;
    public override void Activate(Player parent)
    {
        player = parent;
        motion = player.GetComponent<Motion>();
        amount = 10; // Número de trampas que puede desarmar.
        player.amount = amount;
        isActive = true;
        List<MazeCell> tempCell = new List<MazeCell>();
<<<<<<< HEAD


=======
>>>>>>> 25e17007b4672106cb6948085f4342a9dd8c3e02
    }

    public override void Fast()
    {
        if (player == null || motion == null) return;

        currentCell = motion.GetMazeCell(player.transform.position);
        Debug.Log(currentCell.gameObject.name);
        Debug.Log(currentCell._VioletHole.CompareTag("VioletTrap"));

        if ((currentCell._VioletHole.CompareTag("VioletTrap") || currentCell._GreenSpike.CompareTag("GreenTrap") || currentCell._InvisibleTrap.CompareTag("InvisibleTrap")) && !tempCell.Contains(currentCell))
        {
            tempCell.Add(currentCell);

            currentCell._GreenSpike.SetActive(false); currentCell._GreenSpike.tag = "Untagged";
            currentCell._InvisibleTrap.SetActive(false); currentCell._InvisibleTrap.tag = "Untagged";
            currentCell._VioletHole.SetActive(false); currentCell._VioletHole.tag = "Untagged";
            currentCell._NormalFloor.SetActive(true);

            player.amount--;

            Debug.Log($"Trampa desactivada. Trampas restantes: {player.amount}");

            // Si no quedan trampas por desarmar, termina la habilidad.
            if (player.amount <= 0)
            {
                player.CoolDown = CoolDown; // Aplica el enfriamiento.
                Debug.Log("Habilidad completada. Entrando en enfriamiento.");
            }
          
        }
    }
}
