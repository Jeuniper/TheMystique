using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;//物体的骨骼动画
    public AnimationReferenceAsset 
        idle, longIdle1, longIdle2, run ;//声明所有的动画
    public CharacterState currentState;//当前玩家的状态
    public string currentAnimation;//获取当前的动画进度
    public float runSpeed = 5;//移动速度
    public float jumpSpeed = 3;//跳跃速度
    bool isJump = false;//是否在跳
    public float movement;//获取轴的输入
    private Rigidbody2D rigidBbody;
    private Vector3 rawLocalScale;//原始尺寸
    private float dir=1;//当前的方向


    public enum CharacterState { 
        Idle = 0,
        LongIdle = 1,
        Run = 2,
        FastRun = 3,
        Examine = 4,
        Attack = 5
    }
    // Start is called before the first frame update
    void Start()
    {
        rawLocalScale = transform.localScale;
        rigidBbody = GetComponent<Rigidbody2D>();
        currentState = CharacterState.Idle;
        SetCharacterState(CharacterState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        Flip();//检测翻转
        Move();//检测移动
        IsJump();//检测跳跃

    }
    
    
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name == currentAnimation)
        {
            return;
            //如果动画状态符合状态，则直接返回结束。
        }
        //用于播放想要播放的动画
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;//设置动画的轨迹id，动画名称，loop状态以及播放速度
        currentAnimation= animation.name;//更新动画名称
    }
    public void SetCharacterState(CharacterState state)
    {
        //用于设置角色的状态
        if (state == CharacterState.Idle)
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state == CharacterState.Run)
        {
            SetAnimation(run, true, 1f);
        }
    }

    public void Flip()
    {
        //左右翻转
        if (movement < 0)
        {
            transform.localScale = new Vector3(dir * rawLocalScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(dir * rawLocalScale.x, transform.localScale.y, transform.localScale.z);
        }
   
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");//获取输入
        if (movement < 0) { movement = -1; dir = -1; }
        else if (movement > 0 ){ movement = 1; dir = 1; }


        
        rigidBbody.velocity = new Vector2(movement*runSpeed, rigidBbody.velocity.y);

        if (movement != 0 )//运动
        {
            SetCharacterState(CharacterState.Run);
            
        }
        else
        {
            SetCharacterState(CharacterState.Idle);
        }
    }

    public void Jump()
    {
        Debug.Log(isJump);
        if (!isJump) 
        {
            //如果没有跳跃，跳跃
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            rigidBbody.velocity = Vector2.up * jumpVel;
            
        }

    }
    public void IsJump()
    {

        //检测玩家是否在跳跃（y轴是否有移动）
        if (Mathf.Abs( rigidBbody.velocity.y ) >= 0.001f)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }
}
