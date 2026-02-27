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
        JUMP,
        PET,
        LOOK
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

    private bool isClimb;      //爬
    private bool isJump;       //跳
    private bool canControl;   //可控制
    private bool hasDialog;    //有对话
    private bool isDialog;     //正在对话


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

    public E_LunaMovement LunaMovement { get => lunaMovement; set { lunaMovement = value; } }
    public E_LunaBattle LunaBattle { get { return lunaBattle; } }
    public float HpWidthMap { get { return hpWidthInMap; } }
    public float MpWidthMap { get { return mpWidthInMap; } }
    public bool CanControll { get { return canControl; }set {canControl = value; } }
    public bool IsDialog { get => isDialog; set { isDialog = value; } }


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
        hasDialog = false;
        isDialog = false;

        MOVESPEED = 3;
        moveValue = new Vector3(0, 0);
    }

    protected override void Update()
    {
        moveSpeed = moveValue.magnitude * MOVESPEED;
        if (canControl)
        {
            InputHandle();
        }

        //方向
        if (moveValue.magnitude > 0.1f)
        {
            animatorOutBattle.SetFloat("LookX", moveValue.x);
            animatorOutBattle.SetFloat("LookY", moveValue.y);
        }

        ChangeMoveWay();
    }

    protected override void FixedUpdate()
    {
        position = transformOutBattle.position;
        position += moveSpeed * direction * Time.fixedDeltaTime; //Time.deltaTime 1/当前帧率
        rigidbody2d.MovePosition(position); //通过刚体移动物体
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

        if (Input.GetKeyDown(KeyCode.E) && hasDialog) PressTip();
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
        lunaBattle = lunaBS;
        int state = lunaBattle switch
        {
            E_LunaBattle.IDLE => 0,
            E_LunaBattle.MOVEFORWARD => 1,
            E_LunaBattle.MOVEBACK => 1,
            E_LunaBattle.ATTACK => 2,
            E_LunaBattle.DEFEND => 3,
            E_LunaBattle.HEAL => 4,
            E_LunaBattle.SKILL => 5,
            E_LunaBattle.HIT => 6,
            E_LunaBattle.DIE => 7,
            E_LunaBattle.RUN => 1,
            _ => 0
        }; animatorInBattle.SetInteger("State", state);

        if(state == 1){
            int dir = lunaBattle switch
            {
                E_LunaBattle.MOVEFORWARD => 1,
                E_LunaBattle.MOVEBACK => -1,
                E_LunaBattle.RUN => -1,
                _ => 1
            }; animatorInBattle.SetFloat("Dir", dir);
        }
    }

    private void ChangeMoveWay()
    {
        if (isDialog)
        {
            InteracteAni((int)LunaMovement);
            return;
        }
        if (isJump && !isClimb && !isDialog) {
            MoveAni(5);
            return;
        }
        if (isClimb && !isJump && !isDialog) {
            if (moveSpeed > 1f)
                MoveAni(3);
            else
                MoveAni(4);
            return;
        }
        if (moveSpeed <= 1f) {
            MoveAni(0);
        }
        else if (MOVESPEED == WALKSPEED) {
            MoveAni(1);
        }
        else if (MOVESPEED == RUNSPEED) {
            MoveAni(2);
        }
    }
    public void Climb(bool isClimb)
    {
        this.isClimb = isClimb;
    }

    public void Jump(bool isJump)
    {
        this.isJump = isJump;
    }
    private void MoveAni(int movement = 0)
    {
        lunaMovement = movement switch
        {
            0 => E_LunaMovement.IDLE,
            1 => E_LunaMovement.WALK,
            2 => E_LunaMovement.RUN,
            3 => E_LunaMovement.CLIMB,
            4 => E_LunaMovement.CLIMB_IDLE,
            5 => E_LunaMovement.JUMP,
            7 => E_LunaMovement.LOOK,
            _ => E_LunaMovement.IDLE
        }; animatorOutBattle.SetInteger("moveWay", movement);
    }

    public override void InteracteAni(int isInteracte = 7)
    {
        lunaMovement = isInteracte switch
        {
            6 => E_LunaMovement.PET,
            7 => E_LunaMovement.LOOK,
            _ => E_LunaMovement.IDLE
        }; animatorOutBattle.SetInteger("moveWay", isInteracte);
    }

    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="collision">有对话的npc</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<DialogBase>() != null)
        {
            dialog = collision.transform.GetComponent<DialogBase>();
            Debug.Log(dialog.DiaIndex);
            if (dialog.DiaIndex != -1){
                hasDialog = true;
                UIManager.Instance.ShowOrHidePressTip(dialog, hasDialog);
            }
        }
    }
    private void PressTip()
    {
        if (hasDialog)
        {
            dialog.ConIndex = 0;
            TalkManager.Instance.LoadDialog(dialog);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dialog.DiaIndex == -1)
        {
            hasDialog = false;
            UIManager.Instance.ShowOrHidePressTip(dialog, hasDialog);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<DialogBase>() != null)
        {
            hasDialog = false;
            UIManager.Instance.ShowOrHidePressTip(dialog, hasDialog);
        }
    }
    public void OnPanleButtonClicked()
    {
        dialog.ContinueDialogConditions();
    }
}