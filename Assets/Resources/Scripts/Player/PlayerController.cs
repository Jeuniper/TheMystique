using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 5;
    public float jumpSpeed = 5;
    public float fallSpeed = -2;
    public float doubleJumpSpeed = 3;
    public float attackSppeed = 2;//攻击时移动的补偿速度
    public int atk = 3;

    private Rigidbody2D myRigidBody;
    private Animator myAni;
    private BoxCollider2D myFeet;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool IsAttack;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Flip();
        Run();
        Jump();
        Fall();
        //Attack();
  


    }
    void CheckGrounded()
    {
        //检测是否是地面
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        myAni.SetBool("IsGrounded", isGrounded);

    }
    void Flip()
    {
        //翻转预制体，只有移动时翻转
        bool HaveHorizontapSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (HaveHorizontapSpeed)
        {
            if (myRigidBody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (myRigidBody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }

    }

    void Run()
    {
        if (IsAttack == false)//不允许在攻击中进行移动
        {
            int v;
            //获取水平轴的输入方向
            float moveDir = Input.GetAxis("Horizontal");
            if (moveDir > 0)
            {
                v = 1;
            }
            else if (moveDir == 0)
            {
                v = 0;
            }
            else
            {
                v = -1;
            }
            //创建新的移动向量，水平移动时y轴速度保持不变
            Vector2 playerVel = new Vector2(v * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVel;

            bool HaveHorizontapSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon && isGrounded;
            myAni.SetBool("IsRun", HaveHorizontapSpeed);
        }


    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidBody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    myRigidBody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }

            }

                

            
        }
        bool isJump = myRigidBody.velocity.y > 0.0f;
        myAni.SetBool("IsJump", isJump);
    }
    void Fall()
    {
        bool isFall = myRigidBody.velocity.y < fallSpeed;
        myAni.SetBool("IsFall", isFall);
    }

    public void Attack()
    {
        //点击交互按钮进行attack
        if (IsAttack == false)
        {
            IsAttack = true;
            myAni.SetTrigger("NormalAttack");
            myRigidBody.velocity = new Vector2(transform.localPosition.x*attackSppeed, 0.0f);

            if (transform.localRotation == Quaternion.Euler(0, 0, 0))//向右
            {
                myRigidBody.velocity = new Vector2(attackSppeed, myRigidBody.velocity.y);
            }
            else if (transform.localRotation == Quaternion.Euler(0, -180, 0)|| transform.localRotation == Quaternion.Euler(0, 180, 0))//向左
            {
                myRigidBody.velocity = new Vector2(-attackSppeed, myRigidBody.velocity.y);
            }
        }
        
    }
    void AttackOver()
    {
        IsAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //敌人进入攻击范围时，敌人受伤
            if (transform.localRotation == Quaternion.Euler(0, 0, 0))//向右击打
            {
                other.GetComponent<Enemy>().GetHit(new Vector2(1, 0));
            }
            if (transform.localRotation == Quaternion.Euler(0, -180, 0))//向右击打
            {
                other.GetComponent<Enemy>().GetHit(new Vector2(-1, 0));
            }
            //给敌人减血
            other.GetComponent<Enemy>().TakeDamage(atk);
        }
    }
}
