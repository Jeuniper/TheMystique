using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    public int damage;
    public GameObject blood;

    public float speed=3;//敌人击退的速度
    private Vector2 direction;//敌人被击退的方向
    private bool isHit;//是否受击
    private AnimatorStateInfo info;//获取动画进度


    private Animator animator;
    private Rigidbody2D Enemyrigidbody;

    private SpriteRenderer sr;
    private Color originalColor;//记录精灵原始的颜色，用于改变颜色

    // Start is called before the first frame update
    protected void Start()
    {
        //获取动画进度和刚体
        animator = transform.GetComponent<Animator>();
        Enemyrigidbody = transform.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();//获取到原始的组件
        originalColor = sr.color;

    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0)
        {
            //死亡后卸载物体
            Destroy(gameObject);
        }
        info = animator.GetCurrentAnimatorStateInfo(0);
        if(isHit)
        {
            Enemyrigidbody.velocity = direction * speed;
            
            if (info.normalizedTime >= 1f)
            {
                isHit = false;
                //在动画播放到0.6s时结束受击状态
            }
        }
    }

    public void GetHit(Vector2 Hitdirction)
    {
        animator.SetTrigger("Hit");
        //将敌人方向朝向伤害来源方向
        transform.localScale =new Vector3(-Hitdirction.x*transform.localScale .x, transform .localScale .y, transform.localScale.z);
        isHit = true;
        this.direction = Hitdirction;
        //获取碰撞体父物体的信息
        
        
    }

    public void TakeDamage(int damage)
    { 
        health -= damage;
        FlashColor(0.1f);
        Instantiate(blood, this.transform.position, Quaternion.identity);
        //生成粒子特效
    }

    void FlashColor(float time)//传入闪烁的时间
    {
        sr.color = Color.red;
        //延迟一点时间后重置颜色为初始状态
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }

}
