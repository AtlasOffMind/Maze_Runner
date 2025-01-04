using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MazeRunner;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Player _PlayerPrefab;
    public LayerMask detectionLayer; // Filtrar objetos por Layer
    public LayerMask wallLayer;// Layer de las paredes para detectar colisiones
    public LayerMask playerLayer;    

    private bool isMoving = false;
    private MazeGenerator mazeGenerator;
    private Vector3 targetPosition; // Posición objetivo del jugador
    private MazeCell currentCell;
    private float moveDistance = 1f; // La distancia que se mueve el jugador
    private IEnumerator currentMovement;

    private int originalLifePoints;
    private List<MazeCell> entrance;
    private List<MazeCell> exit;
    private MazeCell[,] _MazeGrid;
    void Start()
    {
        _PlayerPrefab.GettingSetting();
        originalLifePoints = _PlayerPrefab.lifePionts;

        mazeGenerator = FindFirstObjectByType<MazeGenerator>();
        entrance = mazeGenerator.GetEntrance();
        exit = mazeGenerator.GetExit();
        _MazeGrid = mazeGenerator.GetMatrix();

        //Print();

    }
    void Update()
    {

        if (!isMoving)
        {
            // Detecta la entrada del jugador para moverse
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) TryMove(Vector3.left);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) TryMove(Vector3.right);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) TryMove(Vector3.back);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) TryMove(Vector3.forward);
        }
        if (_PlayerPrefab.lifePionts == 0)
        {
            Restart();
            _PlayerPrefab.lifePionts = originalLifePoints;
        }
    }

    private void TryMove(Vector3 direction)
    {
        // Calcula la nueva posición objetivo
        targetPosition = transform.position + direction * moveDistance;

        // Realiza un raycast en la dirección deseada
        if (!Physics.Raycast(transform.position, direction, moveDistance, wallLayer) && !Physics.Raycast(transform.position, direction, moveDistance, playerLayer) && _PlayerPrefab.steps > 0 )
        {
            if (currentMovement != null) StopCoroutine(currentMovement); // Detiene corrutinas previas
            currentMovement = MoveToTarget(); // Guarda la nueva corrutina
            StartCoroutine(currentMovement);
            _PlayerPrefab.steps--;

            currentCell = GetMazeCell(targetPosition);

            Traps.TrapIsActive(_PlayerPrefab, currentCell);
        }
        else if (Physics.Raycast(transform.position, direction, moveDistance, wallLayer))
        {
            UnityEngine.Debug.Log("Hay una pared en esa dirección.");
        }
        else if ( _PlayerPrefab.steps == 0)
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
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f);
            yield return null;
        }

        // Ajusta la posición final y permite más movimientos
        transform.position = targetPosition;
        isMoving = false;
    }

    private void Restart()
    {
        int random = 0;
        int lastEntrance = -1;
        do
        {
            random = UnityEngine.Random.Range(0, 4);

        } while (random == lastEntrance);
        lastEntrance = random;

        if (currentMovement != null) StopCoroutine(currentMovement);

        transform.position = new Vector3(entrance[random].transform.position.x, transform.position.y, entrance[random].transform.position.z);
        isMoving = false;

    }

    MazeCell GetMazeCell(Vector3 targetPosition) => _MazeGrid[(int)targetPosition.x, (int)targetPosition.z];
    void Print()
    {
        UnityEngine.Debug.Log(0 + "=" + entrance[0].transform.position);
        UnityEngine.Debug.Log(1 + "=" + entrance[1].transform.position);
        UnityEngine.Debug.Log(2 + "=" + entrance[2].transform.position);
        UnityEngine.Debug.Log(3 + "=" + entrance[3].transform.position);

    }
}
