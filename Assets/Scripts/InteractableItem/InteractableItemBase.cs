using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class InteractiableItemBase : MonoBehaviour
{
    protected string targetTag = "Player";
    [Header("交互设置")]
    [SerializeField] protected GameObject effectPrefab;       // 交互时产生的特效预制体
    [SerializeField] protected bool destroyAfterInteract = true; // 是否交互后销毁

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;
        if (!CanInteract(collision)) return;

        if (effectPrefab)
        {
            Instantiate(effectPrefab, collision.GetComponentInChildren<SpriteRenderer>().transform);
        }
        PerformEffect(collision);
        AfterInteract(collision);
    }

    /// <summary>
    /// 判断是否可以交互，虚函数，子类可重写
    /// </summary>
    protected virtual bool CanInteract(Collider2D playerCollider)
    {
        return playerCollider.GetComponent<LunaController>().HP < playerCollider.GetComponent<LunaController>().MaxHP;
    }

    /// <summary>
    /// 执行主要交互效果，抽象方法，由子类实现
    /// </summary>
    protected abstract void PerformEffect(Collider2D playerCollider);

    /// <summary>
    /// 交互后逻辑，虚函数，子类可重写
    /// </summary>
    protected virtual void AfterInteract(Collider2D playerCollider) {
        if (destroyAfterInteract) Destroy(gameObject);
    }
}