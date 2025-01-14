using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    int CoolDown = 0;
    public KeyCode key;
    TurnManagement TM;
    Player player;

    void Update()
    {
        if (TM == null)
        {
            TM = FindAnyObjectByType<TurnManagement>();
        }

        player = TM.GetPlayer();

        if (player.CoolDown == 0)
        {
            if (Input.GetKeyDown(key))
            {
                ability.Activate(player);
                CoolDown = ability.CoolDown;

            }
        }
        
        if (Input.GetKeyDown(key) && player.CoolDown != 0)
        {
            Debug.Log("La habilidad aun no se puede usar");
        }
        
        if (player.amount != 0 && ability.name == "Disarmer")
        {
            ability.Fast();
        }
    }
}
