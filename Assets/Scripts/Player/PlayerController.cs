using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;//����Ĺ�������
    public AnimationReferenceAsset 
        idle, longIdle1, longIdle2, run ;//�������еĶ���
    public CharacterState currentState;//��ǰ��ҵ�״̬
    public string currentAnimation;//��ȡ��ǰ�Ķ�������
    public float runSpeed = 5;//�ƶ��ٶ�
    public float jumpSpeed = 3;//��Ծ�ٶ�
    bool isJump = false;//�Ƿ�����
    public float movement;//��ȡ�������
    private Rigidbody2D rigidBbody;
    private Vector3 rawLocalScale;//ԭʼ�ߴ�
    private float dir=1;//��ǰ�ķ���


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
        Flip();//��ⷭת
        Move();//����ƶ�
        IsJump();//�����Ծ

    }
    
    
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name == currentAnimation)
        {
            return;
            //�������״̬����״̬����ֱ�ӷ��ؽ�����
        }
        //���ڲ�����Ҫ���ŵĶ���
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;//���ö����Ĺ켣id���������ƣ�loop״̬�Լ������ٶ�
        currentAnimation= animation.name;//���¶�������
    }
    public void SetCharacterState(CharacterState state)
    {
        //�������ý�ɫ��״̬
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
        //���ҷ�ת
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
        movement = Input.GetAxis("Horizontal");//��ȡ����
        if (movement < 0) { movement = -1; dir = -1; }
        else if (movement > 0 ){ movement = 1; dir = 1; }


        
        rigidBbody.velocity = new Vector2(movement*runSpeed, rigidBbody.velocity.y);

        if (movement != 0 )//�˶�
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
            //���û����Ծ����Ծ
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            rigidBbody.velocity = Vector2.up * jumpVel;
            
        }

    }
    public void IsJump()
    {

        //�������Ƿ�����Ծ��y���Ƿ����ƶ���
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
