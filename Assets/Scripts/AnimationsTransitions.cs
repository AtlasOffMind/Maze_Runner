using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

public class AnimationsTransitions : MonoBehaviour
{
    private Animator anim;
    public Player player;
    private Motion m;
    public bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {

        if (!inMenu)
        {
            anim = player.GetComponentInChildren<Animator>();
        }
        else anim = gameObject.GetComponent<Animator>();

        if (inMenu && player._HeWons) anim.SetBool("wons", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMenu)
        {
            if (player.inTurn)  m = player.GetComponent<Motion>(); 

            if (player.inTurn && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                anim.SetBool("moving", true);
            }
            else anim.SetBool("moving", false);
        }
    }
}
