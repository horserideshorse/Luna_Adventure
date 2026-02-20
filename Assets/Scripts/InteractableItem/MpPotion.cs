using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpPotion : InteractiableItemBase
{
    [Header("MP“©ÀÆ…Ë÷√")]
    [SerializeField] private int healAmount = 1;

    protected override void PerformEffect(Collider2D playerCollider)
    {
        LunaController lunaController = playerCollider.GetComponent<LunaController>();
        if (lunaController != null)
        {
            ControllerBase.ChangeMP(lunaController, healAmount);
        }
    }

    protected override bool CanInteract(Collider2D playerCollider)
    {
        return playerCollider.GetComponent<LunaController>().MP < playerCollider.GetComponent<LunaController>().MaxMP;
    }
}
