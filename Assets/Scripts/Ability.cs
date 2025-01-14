using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public int CoolDown;
    public bool isOn { get; private set; }


    public virtual void Activate(Player parent)
    { }

    public virtual void Fast()
    { }

    public void SetOn(bool i)
    {isOn = i;} 

}
