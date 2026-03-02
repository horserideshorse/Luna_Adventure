using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteracterableArea : MonoBehaviour
{
    protected string targetTag = "Player";

    protected LunaController lunaController;
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        lunaController = collision.GetComponent<LunaController>();
        if (!collision.CompareTag(targetTag)) return;
        PerformEffect();
    }

    /// <summary>
    /// 虚函数，子类可重写
    /// </summary>
    protected virtual void OnTriggerExit2D(Collider2D collision) {  }

    /// <summary>
    /// 执行主要交互效果，抽象方法，由子类实现
    /// </summary>
    protected abstract void PerformEffect();

    /// <summary>
    /// 交互后逻辑，抽象方法，由子类实现
    /// </summary>
    protected abstract void AfterInteract();
}
