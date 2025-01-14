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

        if (player.CoolDown == 0)
        {
            if (Input.GetKeyDown(key))
            {
                ability.Activate(player);
                CoolDown = ability.CoolDown;
            }
        }

        if (Input.GetKeyDown(key) && player.CoolDown != 0) { Debug.Log("La habilidad aun no se puede usar"); }

        if (player.amount != 0 && ability.name == "Disarmer") { ability.Fast(); }

        //if (ability.name == "CopyCat" && ability.isOn) { ability.Fast(); }

        if (savedAbility.name == "CopyCat" && player.CoolDown != 0) { ability = savedAbility; }
    }
    public void SaveAbility(Ability first, Ability second)
    {
        savedAbility = first;
        ability = second;
        ability.Activate(player);
    }
}
