using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class LunaController : ControllerBase
{
    public enum E_LunaMovement
    {
        IDLE,
        WALK,
        RUN,
        CLIMB,
        CLIMB_IDLE,
        JUMP
    }
    public enum E_LunaBattle
    {
        IDLE,
        MOVEFORWARD,
        ATTACK,
        DEFEND,
        HEAL,
        SKILL,
        HIT,
        DIE,
        RUN,
        MOVEBACK
    }
    private E_LunaMovement lunaMovement;
    private E_LunaBattle lunaBattle;

    private bool isClimb;
    private bool isJump;
    private bool canControl;
    private bool isDialog;

    private float WALKSPEED = 3;
    private float RUNSPEED = 6;
    private float MOVESPEED;    //移动常数
    private Vector3 moveValue;  //移动系数

    private DialogBase dialog;

    [Header("UI")]
    public Text text_HP_value;
    public Text text_MP_value;
    public Image hpMaskMap;
    public Image mpMaskMap;
    protected float hpWidthInMap;
    protected float mpWidthInMap;

    //[Header("属性")]
    //[SerializeField] private int normalDamage = -1;
    //[SerializeField] private int skillDamage = -2;
    //[SerializeField] private int heal = 2;

    public E_LunaMovement LunaMovement { get { return lunaMovement; } }
    public E_LunaBattle LunaBattle { get { return lunaBattle; } }
    public float HpWidthMap { get { return hpWidthInMap; } }
    public float MpWidthMap { get { return mpWidthInMap; } }
    public bool CanControll { get { return canControl; }set {canControl = value; } }

    protected override void Awake()
    {
        base.Awake();

        rigidbody2d = GetComponent<Rigidbody2D>();
        dialog = gameObject.GetComponent<DialogBase>();

        hpWidthInMap = hpMaskMap.rectTransform.rect.width;
        mpWidthInMap = mpMaskMap.rectTransform.rect.width;
    }
    protected override void Start()
    {
        
        TalkManager.Instance.LoadDialog(dialog);
        
        ChangeHP(this, MaxHP);
        ChangeMP(this, MaxMP);

        lunaBattle = E_LunaBattle.IDLE;
        lunaMovement = E_LunaMovement.IDLE;

        isClimb = false;
        isJump = false;
        isDialog = false;

        MOVESPEED = 3;
        moveValue = new Vector3(0, 0);
    }

    protected override void Update()
    {
        //方向
        if (moveValue.magnitude > 0.1f)
        {
            animatorOutBattle.SetFloat("LookX", moveValue.x);
            animatorOutBattle.SetFloat("LookY", moveValue.y);
        }

        moveSpeed = moveValue.magnitude * MOVESPEED;
        InputHandle();
        MoveAni();
    }

    protected override void FixedUpdate()
    {
        position = transformOutBattle.position;
        position += moveSpeed * direction * Time.fixedDeltaTime; //Time.deltaTime 1/当前帧率
        rigidbody2d.MovePosition(position); //通过刚体移动物体
    }

    public void Climb(bool isClimb)
    {
        this.isClimb = isClimb;
    }

    public void Jump(bool isJump)
    {
        this.isJump = isJump;
    }

    private void InputHandle()
    {
        direction.x = Input.GetAxisRaw("Horizontal");   //获取水平输入
        direction.y = Input.GetAxisRaw("Vertical");     //获取垂直输入

        moveValue.x = Input.GetAxis("Horizontal");   //获取水平输入
        moveValue.y = Input.GetAxis("Vertical");     //获取垂直输入
        //归一化，限制向量模长
        moveValue = Vector3.ClampMagnitude(moveValue, 1f);
        //左shift
        if (Input.GetKeyDown(KeyCode.LeftShift)) MOVESPEED = RUNSPEED;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) MOVESPEED = WALKSPEED;

        if (Input.GetKeyDown(KeyCode.E) && isDialog) PressTip();
    }
    public IEnumerator LunaAttack(MonsterController target)
    {
        //前进
        BattleAni(E_LunaBattle.MOVEFORWARD);
        transformInBattle.DOLocalMove(target.InitPos + new Vector3(-1.5f, 0), 0.5f)
            .SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        //攻击
        BattleAni(E_LunaBattle.ATTACK);
        yield return new WaitForSecondsRealtime(1f);
        //后撤
        BattleAni(E_LunaBattle.MOVEBACK);
        transformInBattle.DOLocalMove(initPostionInBattle, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        //站立
        BattleAni(E_LunaBattle.IDLE);
    }
    public IEnumerator LunaDefend()
    {
        //防御
        BattleAni(E_LunaBattle.DEFEND);
        yield return new WaitForSecondsRealtime(1.2f);
        transformInBattle.DOLocalMove(initPostionInBattle + new Vector3(-0.5f, 0), 0.2f).SetUpdate(true)
                    .OnComplete(() => { transformInBattle.DOLocalMove(initPostionInBattle, 0.2f).SetUpdate(true); });
        yield return new WaitForSecondsRealtime(1.0f);
        //站立
        BattleAni(E_LunaBattle.IDLE);
    }
    public IEnumerator LunaSkill()
    {
        //技能
        BattleAni(E_LunaBattle.SKILL);
        yield return new WaitForSecondsRealtime(1);
    }
    public IEnumerator LunaHeal()
    {
        //回血
        BattleAni(E_LunaBattle.HEAL);
        yield return new WaitForSecondsRealtime(1);
        //站立
        BattleAni(E_LunaBattle.IDLE);
    }
    public IEnumerator LunaRunAway()
    {
        BattleAni(E_LunaBattle.RUN);
        transformInBattle.DOLocalMove(InitPos + new Vector3(-3f, 0), 1f)
            .SetUpdate(true);
        yield return new WaitForSecondsRealtime(1.2F);
        //站立
        BattleAni(E_LunaBattle.IDLE);
    }
    public override IEnumerator Hurt()
    {
        //受伤
        BattleAni(E_LunaBattle.HIT);
        yield return new WaitForSecondsRealtime(0.5f);
        //站立
        BattleAni(E_LunaBattle.IDLE);
    }
    public override IEnumerator Death()
    {
        BattleAni(E_LunaBattle.DIE);

        yield return new WaitForSecondsRealtime(2f);
        //站立
        ChangeHP(this, MaxHP);
        BattleAni(E_LunaBattle.IDLE);
    }

    public void BattleAni(E_LunaBattle lunaBS)
    {
        this.lunaBattle = lunaBS;
        switch (lunaBS) //状态机
        {
            case E_LunaBattle.IDLE:
                animatorInBattle.SetInteger("State", 0);
                break;
            case E_LunaBattle.MOVEFORWARD:
                animatorInBattle.SetInteger("State", 1);
                animatorInBattle.SetFloat("Dir", 1);
                break;
            case E_LunaBattle.MOVEBACK:
                animatorInBattle.SetInteger("State", 1);
                animatorInBattle.SetFloat("Dir", -1);
                break;
            case E_LunaBattle.ATTACK:
                animatorInBattle.SetInteger("State", 2);
                break;
            case E_LunaBattle.DEFEND:
                animatorInBattle.SetInteger("State", 3);
                break;
            case E_LunaBattle.HEAL:
                animatorInBattle.SetInteger("State", 4);
                break;
            case E_LunaBattle.SKILL:
                animatorInBattle.SetInteger("State", 5);
                break;
            case E_LunaBattle.HIT:
                animatorInBattle.SetInteger("State", 6);
                break;
            case E_LunaBattle.DIE:
                animatorInBattle.SetInteger("State", 7);
                break;
            case E_LunaBattle.RUN:
                animatorInBattle.SetInteger("State", 1);
                animatorInBattle.SetFloat("Dir", -1);
                break;
        }
    }
    private void MoveAni()
    {
        switch (lunaMovement) //状态机
        {
            case E_LunaMovement.IDLE:
                if (moveSpeed > 1)
                {
                    lunaMovement = E_LunaMovement.WALK;
                    animatorOutBattle.SetInteger("moveWay", 1);
                }
                break;

            case E_LunaMovement.WALK:
                if (moveSpeed <= 1)
                {
                    lunaMovement = E_LunaMovement.IDLE;
                    animatorOutBattle.SetInteger("moveWay", 0);
                }
                else if (moveSpeed > 3.1f)
                {
                    lunaMovement = E_LunaMovement.RUN;
                    animatorOutBattle.SetInteger("moveWay", 2);
                }
                else if (isClimb)
                {
                    lunaMovement = E_LunaMovement.CLIMB;
                    animatorOutBattle.SetInteger("moveWay", 3);
                }
                else if (isJump)
                {
                    lunaMovement = E_LunaMovement.JUMP;
                    rigidbody2d.simulated = false;
                    animatorOutBattle.SetInteger("moveWay", 5);
                }
                break;

            case E_LunaMovement.RUN:
                if (moveSpeed <= 3.1f)
                {
                    lunaMovement = E_LunaMovement.WALK;
                    animatorOutBattle.SetInteger("moveWay", 1);
                }
                else if (isClimb)
                {
                    lunaMovement = E_LunaMovement.CLIMB;
                    animatorOutBattle.SetInteger("moveWay", 3);
                }
                else if (isJump)
                {
                    lunaMovement = E_LunaMovement.JUMP;
                    rigidbody2d.simulated = false;
                    animatorOutBattle.SetInteger("moveWay", 5);
                }
                break;

            case E_LunaMovement.CLIMB:
                if (!isClimb)
                {
                    lunaMovement = E_LunaMovement.WALK;
                    animatorOutBattle.SetInteger("moveWay", 1);
                }
                if (moveSpeed <= 1)
                {
                    lunaMovement = E_LunaMovement.CLIMB_IDLE;
                    animatorOutBattle.SetInteger("moveWay", 4);
                }
                break;

            case E_LunaMovement.CLIMB_IDLE:
                if (moveSpeed > 1)
                {
                    lunaMovement = E_LunaMovement.CLIMB;
                    animatorOutBattle.SetInteger("moveWay", 3);
                }
                break;

            case E_LunaMovement.JUMP:
                if (!isJump)
                {
                    lunaMovement = E_LunaMovement.WALK;
                    rigidbody2d.simulated = true;
                    animatorOutBattle.SetInteger("moveWay", 1);
                }
                break;

            default:
                lunaMovement = E_LunaMovement.IDLE;
                animatorOutBattle.SetInteger("moveWay", 0);
                break;
        }
    }
    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="collision">有对话的npc</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<DialogBase>() != null)
        {
            isDialog = true;
            dialog = collision.transform.GetComponent<DialogBase>();
            UIManager.Instance.ShowOrHidePressTip(dialog, isDialog);
        }
    }
    private void PressTip(){
        if (isDialog) TalkManager.Instance.LoadDialog(dialog);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<DialogBase>() != null)
        {
            isDialog = false;
            UIManager.Instance.ShowOrHidePressTip(dialog, isDialog);
        }
    }
    public void OnPanleButtonClicked()
    {
        dialog.ContinueDialogConditions();
    }
}