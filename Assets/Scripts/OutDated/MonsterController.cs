using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterController : ControllerBase
{
    //¼ÆÊ±Æ÷
    private float timer;
    //public Vector3 InitPos;

    protected override void Awake()
    {
        timer = 5;
    }

    protected override void Start()
    {
        base.Start();

        //RandomMove();
    }
    protected override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) RandomMove();
    }
    protected override void FixedUpdate()
    {
        position = rigidbody2d.position;
        position += moveSpeed * direction * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    protected void RandomMove()
    {
        timer = RandomInt(1, 3);

        moveSpeed = RandomInt(-1, 2);
        direction.x = RandomInt(-1, 1);
        direction.y = RandomInt(-1, 1);

        animator.SetFloat("LookX", direction.x);
        animator.SetFloat("LookY", direction.y);
    }
}
