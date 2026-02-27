using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ControllerBase : CharacterBase
{
    [Header("属性")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected int maxMP;
    [SerializeField] protected float atkSpeed;
    protected int currentHP;
    protected int currentMP;
    protected Vector3 initPostionInBattle;
    protected Transform transformInBattle;
    protected Rigidbody2D rigidbody2d;

    [Header("UI")]
    [SerializeField] protected Image hpMaskInBattle;
    [SerializeField] protected Image mpMaskInBattle;
    [SerializeField] protected Animator animatorInBattle;

    
    public int HP { get { return currentHP; } set { currentHP = value; } }
    public int MP { get { return currentMP; } set { currentMP = value; } }
    public int MaxHP { get { return maxHP; } }
    public int MaxMP { get { return maxMP; } }
    public float AtkSp { get { return atkSpeed; } }
    public float HpWidth { get { return hpWidthInBattle; } }
    public float MpWidth { get { return mpWidthInBattle; } }
    public Vector3 InitPos { get { return initPostionInBattle; } }
    public Image HpMask { get { return hpMaskInBattle; } }
    public Image MpMask { get { return mpMaskInBattle; } }

    protected override void Awake()
    {
        base.Awake();
        transformInBattle = animatorInBattle.GetComponent<Transform>();
        initPostionInBattle = transformInBattle.localPosition;
        hpWidthInBattle = hpMaskInBattle.rectTransform.rect.width;
        mpWidthInBattle = mpMaskInBattle.rectTransform.rect.width;
    }

    protected override void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ChangeHP(this, MaxHP);
        ChangeMP(this, MaxMP);
    }
    public static void ChangeHP(ControllerBase changer, int amount)
    {
        changer.GetComponent<ControllerBase>().HP = Mathf.Clamp //Clamp限制范围为[0, 5]
            (changer.GetComponent<ControllerBase>().HP + amount, 0, changer.GetComponent<ControllerBase>().MaxHP);
        UIManager.Instance.SetHpValue(changer.GetComponent<ControllerBase>());
    }
    public static void ChangeMP(ControllerBase changer, int amount)
    {
        changer.GetComponent<ControllerBase>().MP = Mathf.Clamp
            (changer.GetComponent<ControllerBase>().MP + amount, 0, changer.GetComponent<ControllerBase>().MaxHP);
        UIManager.Instance.SetMpValue(changer);
    }
    public override IEnumerator Hurt() { yield return null; }
    public override IEnumerator Death()
    {
        Destroy(gameObject);
        DOTween.Kill(gameObject.GetComponent<SpriteRenderer>());
        yield return null;
    }
}
