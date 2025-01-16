using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using MazeRunner;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private Player _CurrentPlayer;

    public LayerMask detectionLayer; // Filtrar objetos por Layer
    public LayerMask wallLayer;// Layer de las paredes para detectar colisiones
    public LayerMask playerLayer;

    private bool isMoving = false;

    private MazeGenerator mazeGenerator;
    private Vector3 targetPosition; // Posición objetivo del jugador
    private MazeCell currentCell;
    private float moveDistance = 1f; // La distancia que se mueve el jugador
    public IEnumerator currentMovement;


    private int originalLifePoints = 0;
    private List<MazeCell> entrance;
    //private List<MazeCell> exit;
    private MazeCell[,] _MazeGrid;

    private TurnManagement TM;

    int lastEntrance = -1;



    void Start()
    {
        TM = FindFirstObjectByType<TurnManagement>();
        mazeGenerator = FindFirstObjectByType<MazeGenerator>();

        //exit = mazeGenerator.GetExit();

        entrance = mazeGenerator.GetEntrance();
        _MazeGrid = mazeGenerator.GetMatrix();

        _CurrentPlayer = TM.GetPlayer();

        originalLifePoints = _CurrentPlayer.lifePoints;
    }
    void Update()
    {
        _CurrentPlayer = TM.GetPlayer();

        if (!isMoving && _CurrentPlayer.inTurn == true && _CurrentPlayer.penaltyTurn == 0)
        {
            // Detecta la entrada del jugador para moverse
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) TryMove(Vector3.left);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) TryMove(Vector3.right);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) TryMove(Vector3.back);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) TryMove(Vector3.forward);
        }
        if ((_CurrentPlayer.steps == 0 && !isMoving) || (_CurrentPlayer.penaltyTurn != 0 && !isMoving))
        {
            TM.EndTurn(); // Cambia al siguiente jugador cuando se quede sin pasos
            _CurrentPlayer = TM.GetPlayer();
        }
        if (_CurrentPlayer.lifePoints == 0)
        {
            Restart();
            _CurrentPlayer.lifePoints = originalLifePoints;
        }
    }

    private void TryMove(Vector3 direction)
    {
        // Calcula la nueva posición objetivo
        targetPosition = _CurrentPlayer.transform.position + direction * moveDistance;

        // Realiza un raycast en la dirección deseada
        if (!Physics.Raycast(_CurrentPlayer.transform.position, direction, moveDistance, wallLayer) && !Physics.Raycast(_CurrentPlayer.transform.position, direction, moveDistance, playerLayer) && _CurrentPlayer.steps > 0)
        {
            if (currentMovement != null) StopCoroutine(currentMovement); // Detiene corrutinas previas
            currentMovement = MoveToTarget(); // Guarda la nueva corrutina
            StartCoroutine(currentMovement);

            _CurrentPlayer.steps--;

            currentCell = GetMazeCell(targetPosition);

            Traps.TrapIsActive(_CurrentPlayer, currentCell);
        }
        else if (Physics.Raycast(_CurrentPlayer.transform.position, direction, moveDistance, wallLayer))
        {
            UnityEngine.Debug.Log("Hay una pared en esa dirección.");
        }
        else if (_CurrentPlayer.steps == 0)
        {
            UnityEngine.Debug.Log("No puedes dar mas pasos.");
        }
        else
        {
            UnityEngine.Debug.Log("Hay un jugador en esa casilla");
        }

    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        // Movimiento fluido hacia la posición objetivo
        while (Vector3.Distance(_CurrentPlayer.transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(_CurrentPlayer.transform.position, targetPosition, Time.deltaTime * 5f);
            yield return null;
        }

        // Ajusta la posición final y permite más movimientos
        _CurrentPlayer.transform.position = targetPosition;
        isMoving = false;
    }

    private void Restart()
    {
        int random = 0;
        do
        {
            random = UnityEngine.Random.Range(0, 4);

        } while (random == lastEntrance);
        lastEntrance = random;

        if (currentMovement != null) StopCoroutine(currentMovement);

        _CurrentPlayer.transform.position = new Vector3(entrance[random].transform.position.x, _CurrentPlayer.transform.position.y, entrance[random].transform.position.z);
        isMoving = false;
    }
    public void StopMovement() { StopCoroutine(currentMovement); isMoving = false; }


    public MazeCell GetMazeCell(Vector3 targetPosition) => _MazeGrid[(int)targetPosition.x, (int)targetPosition.z];

    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Llegaste a la salida");
    }


}
