using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

[CreateAssetMenu]
public class Control : Ability
{
    Player player;
    Player otherplayer;

    Motion motion;
    MazeCell mazeCell;
    MazeCell newMazeCell;
    Motion M;
    TurnManagement turnManagement;
    MazeGenerator Maze;
    public override void Activate(Player parent)
    {
        player = parent;
        motion = player.GetComponent<Motion>();
        M = player.GetComponent<Motion>();
        turnManagement = FindFirstObjectByType<TurnManagement>();
        Maze = FindFirstObjectByType<MazeGenerator>();

        mazeCell = motion.GetMazeCell(player.transform.position);
        newMazeCell = mazeCell;
        mazeCell._SelectionCube.SetActive(true);

        M.isSelecting = true;

        SetOn(true);

        player.CoolDown = CoolDown; // Aplica el enfriamiento.

    }
    public override void Fast()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            mazeCell._SelectionCube.SetActive(false);
            newMazeCell = motion.GetMazeCell(new Vector3(mazeCell.transform.position.x - 1, mazeCell.transform.position.y, mazeCell.transform.position.z));
            mazeCell = newMazeCell;
            mazeCell._SelectionCube.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            mazeCell._SelectionCube.SetActive(false);
            newMazeCell = motion.GetMazeCell(new Vector3(mazeCell.transform.position.x + 1, mazeCell.transform.position.y, mazeCell.transform.position.z));
            mazeCell = newMazeCell;
            mazeCell._SelectionCube.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mazeCell._SelectionCube.SetActive(false);
            newMazeCell = motion.GetMazeCell(new Vector3(mazeCell.transform.position.x, mazeCell.transform.position.y, mazeCell.transform.position.z - 1));
            mazeCell = newMazeCell;
            mazeCell._SelectionCube.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            mazeCell._SelectionCube.SetActive(false);
            newMazeCell = motion.GetMazeCell(new Vector3(mazeCell.transform.position.x, mazeCell.transform.position.y, mazeCell.transform.position.z + 1));
            mazeCell = newMazeCell;
            mazeCell._SelectionCube.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (turnManagement.ContainsPlayer(mazeCell.transform.position) && !Maze.NearExit(mazeCell.transform.position))
            {
                otherplayer = turnManagement.GetPlayer(mazeCell.transform.position);

                Vector3 pos1 = player.transform.position;

                Vector3 pos2 = otherplayer.transform.position;

                player.transform.position = pos2;
                otherplayer.transform.position = pos1;

                mazeCell._SelectionCube.SetActive(false);

                M.isSelecting = false;
                SetOn(false);
            }
        }
    }
}
