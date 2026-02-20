using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LunaManager : CharacterManager<LunaManager>
{
    //public enum E_LunaBattle
    //{
    //    IDLE,
    //    MOVEFORWARD,
    //    ATTACK,
    //    DEFEND,
    //    HEAL,
    //    SKILL,
    //    HIT,
    //    DIE,
    //    RUN,
    //    MOVEBACK
    //}
    //protected E_LunaBattle lunaBattle;

    //[Header("属性")]
    //[SerializeField] protected int normalDamage = -1;
    //[SerializeField] protected int skillDamage = -2;
    //[SerializeField] protected int heal = 2;

    //public E_LunaBattle LunaBS { get { return lunaBattle; } }

    //protected override void Awake()
    //{
    //    base.Awake();
    //}
    //protected override void Start()
    //{
    //    lunaBattle = E_LunaBattle.IDLE;
    //    ChangeHP(MaxHP);
    //    ChangeMP(MaxMP);
    //}

    //public IEnumerator LunaAttack(int value = -1)
    //{
    //    //前进
    //    BattleAni(E_LunaBattle.MOVEFORWARD);
    //    transform.DOLocalMove(MonsterManager.Instance.InitPos + new Vector3(-1.5f, 0, 0), 0.5f)
    //        .SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    //攻击
    //    BattleAni(E_LunaBattle.ATTACK);
    //    yield return new WaitForSecondsRealtime(1f);
    //    //后撤
    //    BattleAni(E_LunaBattle.MOVEBACK);
    //    transform.DOLocalMove(initPostionInBattle, 0.5f).SetUpdate(true);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    //站立
    //    BattleAni(E_LunaBattle.IDLE);
    //}
    //public IEnumerator LunaDefend()
    //{
    //    //防御
    //    BattleAni(E_LunaBattle.DEFEND);
    //    yield return new WaitForSecondsRealtime(1.2f);
    //    transform.DOLocalMove(initPostionInBattle + new Vector3(-0.5f, 0, 0), 0.2f).SetUpdate(true)
    //                .OnComplete(() => { transform.DOLocalMove(initPostionInBattle, 0.2f).SetUpdate(true); });
    //    yield return new WaitForSecondsRealtime(1.0f);
    //    //站立
    //    BattleAni(E_LunaBattle.IDLE);
    //}
    //public IEnumerator LunaSkill()
    //{
    //    //技能
    //    BattleAni(E_LunaBattle.SKILL);
    //    yield return new WaitForSecondsRealtime(1);
    //}
    //public IEnumerator LuanHeal()
    //{
    //    //回血
    //    BattleAni(E_LunaBattle.HEAL);
    //    yield return new WaitForSecondsRealtime(1);
    //    //站立
    //    BattleAni(E_LunaBattle.IDLE);
    //}
    //public IEnumerator LunaHurt()
    //{
    //    //受伤
    //    BattleAni(E_LunaBattle.HIT);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    //站立
    //    BattleAni(E_LunaBattle.IDLE);
    //}
    //public override void Death(GameObject gameObject = null)
    //{
    //    ChangeHP(MaxHP);
    //}

    //public void BattleAni(E_LunaBattle lunaBS)
    //{
    //    this.lunaBattle = lunaBS;
    //    switch (lunaBS) //状态机
    //    {
    //        case E_LunaBattle.IDLE:
    //            animatorInBattle.SetInteger("State", 0);
    //            break;
    //        case E_LunaBattle.MOVEFORWARD:
    //            animatorInBattle.SetInteger("State", 1);
    //            animatorInBattle.SetFloat("Dir", 1);
    //            break;
    //        case E_LunaBattle.MOVEBACK:
    //            animatorInBattle.SetInteger("State", 1);
    //            animatorInBattle.SetFloat("Dir", -1);
    //            break;
    //        case E_LunaBattle.ATTACK:
    //            animatorInBattle.SetInteger("State", 2);
    //            break;
    //        case E_LunaBattle.DEFEND:
    //            animatorInBattle.SetInteger("State", 3);
    //            break;
    //        case E_LunaBattle.HEAL:
    //            animatorInBattle.SetInteger("State", 4);
    //            break;
    //        case E_LunaBattle.SKILL:
    //            animatorInBattle.SetInteger("State", 5);
    //            break;
    //        case E_LunaBattle.HIT:
    //            animatorInBattle.SetInteger("State", 6);
    //            break;
    //        case E_LunaBattle.DIE:
    //            animatorInBattle.SetInteger("State", 7);
    //            break;
    //        case E_LunaBattle.RUN:
    //            animatorInBattle.SetInteger("State", 8);
    //            break;
    //    }
    //}
}
