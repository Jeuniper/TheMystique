using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Enemy
{
    public float speed;//自动移动的速度
    public float waitTime;//等待时间
    private float startWaitTime;//开始时的等待时间

    public Transform movePos;//移动的目标点
    public Transform leftDownPos;//移动范围的左下角
    public Transform rightUpPos;//移动范围的右上角

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

}
