using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MazeRunner
{
    public class MazeCell : MonoBehaviour
    {
        // Variables privadas con el atributo SerializeField para que sean visibles en el inspector de Unity
        [SerializeField]
        private GameObject _LeftWall; // Representa la pared izquierda de la celda del laberinto.

        [SerializeField]
        private GameObject _RigthtWall; // Representa la pared derecha de la celda del laberinto. (esta mal escrito pero x alguna casualidad si lo cambio ya no funciona).

        [SerializeField]
        private GameObject _FronttWall; // Representa la pared frontal de la celda del laberinto. (esta mal escrito pero x alguna casualidad si lo cambio ya no funciona).

        [SerializeField]
        private GameObject _BackWall; // Representa la pared trasera de la celda del laberinto.

        [SerializeField]
        private GameObject _UnvisitedBlock; // Representa un bloque visual que indica si la celda no ha sido visitada.

        [SerializeField]
        public GameObject _NormalFloor;

        [SerializeField]
        public GameObject _GreenSpike;

        [SerializeField]
        public GameObject _VioletHole;

        [SerializeField]
        public GameObject _InvisibleTrap;

        [SerializeField]
        public GameObject _SelectionCube;

        [SerializeField]
        public GameObject _MazeCellTrigger;
        // Propiedad pública de solo lectura que indica si la celda ha sido visitada.
        public bool IsVisited; 

        Renderer BackWallrender;
        Renderer FrontWallrender;
        Renderer LeftWallrender;
        Renderer RightWallrender;
        Renderer NormalFloorrenderer;


        // Método público para marcar la celda como visitada.
        public void Visit()
        {
            IsVisited = true; // Marca la celda como visitada.
            _UnvisitedBlock.SetActive(false); // Oculta el bloque que indica que la celda no ha sido visitada.


            _NormalFloor.SetActive(true);  
            _GreenSpike.SetActive(false);  
            _VioletHole.SetActive(false);  
            _InvisibleTrap.SetActive(false);
            _SelectionCube.SetActive(false); 

            BackWallrender = _BackWall.GetComponentInChildren<Renderer>();
            FrontWallrender = _FronttWall.GetComponentInChildren<Renderer>();
            LeftWallrender = _LeftWall.GetComponentInChildren<Renderer>();
            RightWallrender = _RigthtWall.GetComponentInChildren<Renderer>();
            NormalFloorrenderer = _NormalFloor.GetComponentInChildren<Renderer>();
        }


        // Métodos públicos para eliminar paredes individuales, probablemente usados para abrir caminos en el laberinto.

        public void ClearLeftWall()
        {
            _LeftWall.SetActive(false); // Desactiva la pared izquierda.
        }

        public void ClearRigthtWall()
        {
            _RigthtWall.SetActive(false); // Desactiva la pared derecha.
        }

        public void ClearFronttWall()
        {
            _FronttWall.SetActive(false); // Desactiva la pared frontal.
        }

        public void ClearBackWall()
        {
            _BackWall.SetActive(false); // Desactiva la pared trasera.
        }

        //Metodo para saber si existe una pared.

        public bool HasRigthWall()
        {
            return _RigthtWall.GetComponent<Renderer>();
        }
        public bool HasLeftWall()
        {
            return _LeftWall.GetComponent<Renderer>();
        }
        public bool HasFrontWall()
        {
            return _FronttWall.GetComponent<Renderer>();
        }
        public bool HasBackWall()
        {
            return _BackWall.GetComponent<Renderer>();
        }

        // Método para cambiar el color del material
        public void ChangeColor(Color newColor)
        {
            if (BackWallrender != null)
            {
                BackWallrender.material.color = newColor;  // Cambia el color del material de la celda.
            }
            if (FrontWallrender != null)
            {
                FrontWallrender.material.color = newColor;  // Cambia el color del material de la celda.
            }
            if (LeftWallrender != null)
            {
                LeftWallrender.material.color = newColor;  // Cambia el color del material de la celda.
            }
            if (RightWallrender != null)
            {
                RightWallrender.material.color = newColor;  // Cambia el color del material de la celda.
            }
            if (RightWallrender != null)
            {
                NormalFloorrenderer.material.color = newColor;  // Cambia el color del material de la celda.
            }

        }

    }

}