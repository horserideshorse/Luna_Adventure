using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterManager<T> : MonoBehaviour where T : MonoBehaviour
{
    //public static T Instance;

    //[Header("属性")]
    //[SerializeField] protected int maxHP = 5;
    //[SerializeField] protected int maxMP = 5;
    //[SerializeField] protected float atkSpeed = 1f;

    //protected float hpOriginWidth;
    //protected float mpOriginWidth;

    //protected Vector2 initPostionInBattle;

    //protected Animator animatorInBattle;

    //[Header("UI")]
    //[SerializeField] protected Image hpMaskImage;
    //[SerializeField] protected Image mpMaskImage;

    //public int MaxHP { get { return maxHP; } }
    //public int MaxMP { get { return maxMP; } }
    //public float AtkSp { get { return atkSpeed; } }
    //public float HpWidth { get { return hpOriginWidth; } }
    //public float MpWidth { get { return mpOriginWidth; } }
    //public Vector2 InitPos { get { return initPostionInBattle; } }
    //public Image HpMask { get { return hpMaskImage; } }
    //public Image MpMask { get { return mpMaskImage; } }

    //protected virtual void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this as T;
    //    }
    //    hpOriginWidth = hpMaskImage.rectTransform.rect.width;
    //    mpOriginWidth = mpMaskImage.rectTransform.rect.width;
    //    initPostionInBattle = transform.localPosition;
    //    animatorInBattle = Instance.GetComponent<Animator>();
    //}
    protected virtual void Start() { }

    //public virtual void Death(GameObject gameObject = null)
    //{
    //    Destroy(gameObject);
    //    Debug.Log("Death");
    //}
    //public virtual void ChangeHP(GameObject changer, int amount)
    //{
    //    changer.GetComponent<ControllerBase<T>>().HP = Mathf.Clamp
    //        (changer.GetComponent<ControllerBase<T>>().HP + amount, 0, maxHP); //Clamp限制范围为[0, 5]
    //    UIManager.Instance.SetHpValue(gameObject);
    //}
    //public virtual void ChangeMP(GameObject changer, int amount)
    //{
    //    changer.GetComponent<ControllerBase<T>>().HP = Mathf.Clamp
    //        (changer.GetComponent<ControllerBase<T>>().HP + amount, 0, maxHP); //Clamp限制范围为[0, 5]
    //    UIManager.Instance.SetMpValue(gameObject);
    //}
    
}
