using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeRunner
{
    public class Player : MonoBehaviour
    {
        // Atributos básicos del jugador
        public int steps;
        public GameObject _Player;
        Renderer playerRenderer;
        Rigidbody playerRestrictions;
        public int lifePionts;


        public void GettingSetting()
        {
            playerRestrictions = GetComponent<Rigidbody>();
        }

        public void Freeze()
        {
            playerRestrictions.constraints = RigidbodyConstraints.FreezeAll;
        }
        public void UnFreeze() { playerRestrictions.constraints = RigidbodyConstraints.None; }

        public void ChangeColorPlayer(Color newColor)
        {
            playerRenderer = _Player.GetComponent<Renderer>();

            playerRenderer.material.color = newColor;
        }

    }
}
