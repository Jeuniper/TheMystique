using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//让编辑器来序列化类

public enum StateType//用枚举声明所有的状态
{
    Idle, Patrol, Chase, React, Attack
}


[Serializable]
public class Parameter
{
    //存放敌人的各种参数，通过System来自动序列化类
    public int health;//生命
    public float moveSpeed;//移动速度
    public float chaseSpeed;//追击速度
    public float startWaitTime;//初始等待时间
    public float waitTime;//通用等待时间
    
    public Transform[] patrolPoints;//巡逻范围
    public Transform[] chasePoints;//追击范围
    public Animator animator;

}

public class FSM : MonoBehaviour
{
    public IState currentState;//每一个状态机都需要有一个当前的状态
    public Parameter parameter = new Parameter();

    


    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();//使用字典主策所有的状态
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle, new EnemyIdleState(this));
        states.Add(StateType.Patrol, new EnemyPatrolState(this));
        states.Add(StateType.Chase, new EnemyChaseState());
        states.Add(StateType.React, new EnemyReactState());
        states.Add(StateType.Attack, new EnemyAttackState());

        TransitionState(StateType.Idle);//设置初始值
        parameter.animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        //转移状态时，先执行当前状态的OnExit函数
        if (currentState != null)//前置条件：存在当前状态
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    //各个状态下的通用功能可以都写在这里
    public void FlipTowards(Transform target)//有目标时的转向功能
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)//目标在左
            {
                transform.localScale = new Vector3(-1*transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if (transform.position.x < target.position.x)//目标在左
            {
                transform.localScale = new Vector3(1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        
    }
}
