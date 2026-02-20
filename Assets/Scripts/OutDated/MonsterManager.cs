using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterManager : CharacterManager<MonsterManager>
{
    //protected override void Start()
    //{
    //    ChangeHP(maxHP);
    //    ChangeMP(MaxMP);
    //}
    //public IEnumerator MonsterAttack()
    //{
    //    yield return new WaitForSecondsRealtime(0.5f);//待机

    //    //前进
    //    transform.DOLocalMove(LunaManager.Instance.InitPos + new Vector3(1.5f, 0, 0), 0.5f)
    //        .SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.5f);

    //    //攻击
    //    transform.DOLocalMove(LunaManager.Instance.InitPos, 0.2f).SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.2f);
    //    transform.DOLocalMove(LunaManager.Instance.InitPos + new Vector3(1.5f, 0, 0), 0.5f).SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.5f);

    //    //后撤
    //    transform.DOLocalMove(initPostionInBattle, 0.5f).SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //}

    //public void DestroyMon()
    //{
    //    Destroy(gameObject);
    //    Debug.Log("Destroy");
    //}
}
