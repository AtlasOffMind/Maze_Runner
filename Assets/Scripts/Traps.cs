using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;


namespace MazeRunner
{
    public static class Traps
    {
        public static void TrapIsActive(Player _PlayerPrefab,MazeCell currentCell)
        {
            if (HasTheTrap(currentCell))
            _PlayerPrefab.lifePionts--;
            
        }
        private static bool HasTheTrap(MazeCell currentCell)
        {
            if (currentCell._VioletHole.CompareTag("VioletTrap") || currentCell._InvisibleTrap.CompareTag("InvisibleTrap") || currentCell._GreenSpike.CompareTag("GreenTrap")) return true;
            return false;
        }



    }

}
