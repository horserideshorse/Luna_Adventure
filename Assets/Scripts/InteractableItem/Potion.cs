using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : InteractiableItemBase
{
    [Header("HP“©ÀÆ…Ë÷√")]
    [SerializeField] private int healAmount = 1;

    protected override void PerformEffect(Collider2D playerCollider)
    {
        LunaController lunaController = playerCollider.GetComponent<LunaController>();
        if (lunaController != null)
        {
            ControllerBase.ChangeHP(lunaController, healAmount);
        }
    }
}