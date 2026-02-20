using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAera : InteracterableArea
{
    protected override void PerformEffect()
    {
        lunaController.Climb(true);
    }

    protected override void AfterInteract()
    {
        lunaController.Climb(false);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        AfterInteract();
    }
}
