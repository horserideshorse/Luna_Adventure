using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterController : ControllerBase
{
    //计时器
    private float timer;
    //public Vector3 InitPos;

    protected override void Start()
    {
        base.Start();

        RandomMove();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            BattleManager.Instance.EnterOrExitBattle(true, collision.gameObject, gameObject);
        }
    }

    private void RandomMove()
    {
        timer = RandomInt(1, 3);
        moveSpeed = RandomInt(-1, 2);
        direction.x = RandomInt(-1, 1);
        direction.y = RandomInt(-1, 1);
    }
    public IEnumerator MonsterAttack(ControllerBase target)
    {
        yield return new WaitForSecondsRealtime(0.5f);//待机

        //前进
        transformInBattle.DOLocalMove(target.InitPos + new Vector3(1.5f, 0), 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);

        //攻击
        transformInBattle.DOLocalMove(target.InitPos, 0.2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.2f);
        transformInBattle.DOLocalMove(target.InitPos + new Vector3(1.5f, 0), 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);

        //后撤
        transformInBattle.DOLocalMove(initPostionInBattle, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
    }
    public override IEnumerator Hurt()
    {
        yield return null;
    }

    private int RandomInt(int a, int b)
    {
        return Random.Range(a, b + 1);
    }
}
