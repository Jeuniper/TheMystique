
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;




public class EnemyIdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;//巡逻到点位后的等待时间

    public EnemyIdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter() 
    {

        parameter.animator.Play("Idle");//进入状态时开始idle
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;//开始idle后时间不断增加
        if (timer >= parameter.waitTime)//超过等待时间后开始巡逻
        {
            manager.TransitionState(StateType.Patrol);
        }
    }  
    public void OnExit() 
    {
        timer = 0; 
    }
}
