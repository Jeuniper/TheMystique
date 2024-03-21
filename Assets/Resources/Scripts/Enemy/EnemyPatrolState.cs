
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public class EnemyPatrolState : IState
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPoint;

    public EnemyPatrolState(FSM manager )
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter() 
    {
       
    }

    public void OnUpdate()
    {

    }  
    public void OnExit() 
    {

    }
}
