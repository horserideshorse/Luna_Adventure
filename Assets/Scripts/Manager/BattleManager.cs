using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BattleManager : ManagerBase<BattleManager>
{
    [Header("特效")]
    public GameObject effect_Skill;
    public GameObject effect_Heal;

    [Header("UI")]
    [SerializeField] private GameObject lunaInBattle;
    [SerializeField] private GameObject monsterInBattle;

    private SpriteRenderer lunaSprite;
    private SpriteRenderer monsterSprite;

    private bool playerActionCompleted = false; // 玩家操作是否完成
    private bool lunaActionCompleted = false;   // luna动作是否完成

    void Start(){
        EnterOrExitBattle(false);
    }
    public void EnterOrExitBattle(bool enter = true, GameObject player = null, GameObject enemy = null)
    {

        UIManager.Instance.ShowOrHideBattleGo(enter);
        UIManager.Instance.ShowOrHideBattlePanle(enter);
        UIManager.Instance.ShowOrHideMapGo(!enter);

        if (enter){
            lunaController = player.GetComponent<LunaController>();
            monsterController = enemy.GetComponent<MonsterController>();

            lunaSprite = lunaInBattle.GetComponent<SpriteRenderer>();
            monsterSprite = monsterInBattle.GetComponent<SpriteRenderer>();

            //重置UI
            ControllerBase.ChangeHP(monsterController, 0);
            ControllerBase.ChangeHP(monsterController, 0);
            lunaSprite.DOFade(1f, 0).SetUpdate(true);
            monsterSprite.DOFade(1f, 0).SetUpdate(true);
            lunaInBattle.transform.DOLocalMove(lunaController.InitPos, 0).SetUpdate(true);
            monsterInBattle.transform.DOLocalMove(monsterController.InitPos, 0).SetUpdate(true);

            Time.timeScale = 0f;

            // 根据攻速决定先手
            StartCoroutine(BattleRoutine());
        }
        else
        { 
            Time.timeScale = 1f;
        }   
    }
    public void Attack()
    {
        playerActionCompleted = true;

        StartCoroutine(LunaAttackLogic(-1));
    }

    public void LunaAttack(){
        playerActionCompleted = true;
        StartCoroutine(LunaAttackLogic(-1));
    }
    public void LunaDefend(){
        playerActionCompleted = true;
        StartCoroutine(LunaDefendLogic());
    }
    public void LuanSkill() {
        if (lunaController.MP > 0){
            ControllerBase.ChangeMP(lunaController, -1);
            playerActionCompleted = true;
            StartCoroutine(LunaSkillLogic());
        } 
    }
    public void LuanHeal(){
        if (lunaController.MP > 0){
            ControllerBase.ChangeMP(lunaController, -1);
            playerActionCompleted = true;
            StartCoroutine(LunaHealLogic());
        }
    }
    public void LunaRunAway()
    {
        playerActionCompleted = true;
        StartCoroutine(LunaRunAwayLogic());
    }

    /// <summary>
    /// 战斗主协程：轮流执行回合，直到一方死亡
    /// </summary>
    private IEnumerator BattleRoutine()
    {
        // 比较攻速
        bool lunaFirst = lunaController.AtkSp >= monsterController.AtkSp;

        while (lunaController.HP > 0 && monsterController.HP > 0)
        {
            // Luna 回合
            if (lunaFirst)
            {
                UIManager.Instance.ShowOrHideBattlePanle(true);
                yield return new WaitUntil(() => playerActionCompleted);
                yield return StartCoroutine(LunaTurn());
                if (monsterController.HP <= 0 || lunaController.LunaBattle == LunaController.E_LunaBattle.RUN) 
                    break;
                yield return StartCoroutine(MonsterTurn());
            }
            else
            {
                yield return StartCoroutine(MonsterTurn());
                if (lunaController.HP <= 0) break;
                UIManager.Instance.ShowOrHideBattlePanle(true);
                yield return new WaitUntil(() => playerActionCompleted);
                yield return StartCoroutine(LunaTurn());
            }
            yield return new WaitForSecondsRealtime(0.5f);//待机
        }
        // 战斗结束
    }
    /// <summary>
    /// Luna 回合流程
    /// </summary>
    private IEnumerator LunaTurn()
    {
        UIManager.Instance.ShowOrHideBattlePanle(false);
        lunaActionCompleted = false;

        // 等待玩家动作完成
        yield return new WaitUntil(() => lunaActionCompleted);
        playerActionCompleted = false;

    }

    private IEnumerator MonsterTurn()
    {
        UIManager.Instance.ShowOrHideBattlePanle(false);
        yield return StartCoroutine(MonsterAttackLogic());

    }

    private IEnumerator LunaAttackLogic(int damage = -1)
    {
        StartCoroutine(lunaController.LunaAttack(monsterController));
        // 等待 Luna 攻击动画完成（内含移动、攻击、后撤）
        yield return new WaitForSecondsRealtime(0.8f);

        // 伤害判定
        JudgeAttack(lunaController, monsterController, damage);
        yield return new WaitForSecondsRealtime(1.2f);
        lunaActionCompleted = true;
    }
    private IEnumerator LunaDefendLogic()
    {
        // 防御判定
        StartCoroutine(lunaController.LunaDefend());
        yield return null;

        lunaActionCompleted = true;
    }

    private IEnumerator LunaHealLogic()
    {
        // 播放治疗动画
        StartCoroutine(lunaController.LunaHeal());

        // 生成治疗特效
        GameObject effect = Instantiate(effect_Heal, lunaInBattle.transform);
        effect.transform.localPosition = Vector3.zero;
        yield return new WaitForSecondsRealtime(1);

        // 判定
        ControllerBase.ChangeHP(lunaController, 2);
        lunaActionCompleted = true;
    }
    private IEnumerator LunaSkillLogic()
    {
        // 技能
        yield return StartCoroutine(lunaController.LunaSkill());

        // 技能后接一次强化攻击（伤害 -2）
        StartCoroutine(LunaAttackLogic(-2));

        // 在攻击期间生成技能特效
        yield return new WaitForSecondsRealtime(0.8f);//0.5移动+0.3前摇
        GameObject newGO = Instantiate(effect_Skill, monsterInBattle.transform);
        newGO.transform.localPosition = Vector3.zero;
    }
    private IEnumerator LunaRunAwayLogic()
    {
        StartCoroutine(lunaController.LunaRunAway());

        yield return new WaitForSecondsRealtime(1f);

        // 伤害判定
        JudgeAttack(lunaController, monsterController, 0);
        lunaActionCompleted = true;
    }
    IEnumerator MonsterAttackLogic()
    {
        //monster回合
        StartCoroutine(monsterController.MonsterAttack(lunaController));
        yield return new WaitForSecondsRealtime(1.2f);//0.5待机+0.5前进+0.2前摇
        JudgeAttack(monsterController, lunaController, -1);
        yield return new WaitForSecondsRealtime(1f);
    }

    private void JudgeAttack(ControllerBase attacker, ControllerBase target, int damage)
    {
        SpriteRenderer targetSprite = target == lunaController ? lunaSprite : monsterSprite;

        if (lunaController.LunaBattle == LunaController.E_LunaBattle.DEFEND){
            return;
        }
        else if(lunaController.LunaBattle == LunaController.E_LunaBattle.RUN)
        {
            JudgeTheEnd(target, targetSprite);
            return;
        }
            ControllerBase.ChangeHP(target, damage);//结算
        StartCoroutine(target.Hurt());//受击

        // 受击闪白
        targetSprite.DOFade(0.5f, 0.15f).SetUpdate(true)
            .OnComplete(() => { targetSprite.DOFade(1f, 0.15f).SetUpdate(true)
                .OnComplete(() => { JudgeTheEnd(target, targetSprite); });
            });
    }

    public void JudgeTheEnd(ControllerBase target, SpriteRenderer sprite)
    {
        if(target.HP <= 0){
            StartCoroutine(target.Death());
            sprite.DOFade(0f, 2f).SetUpdate(true)
            .OnComplete
            (() => {
                EnterOrExitBattle(false);
            });
        }
        else if(lunaController.LunaBattle == LunaController.E_LunaBattle.RUN)
        {
            EnterOrExitBattle(false);
        }
    } 
}