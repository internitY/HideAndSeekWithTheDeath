using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual void OnStateEnter()
    {

    }
    public abstract void UpdateState();
    public virtual void OnStateExit()
    {

    }
}
