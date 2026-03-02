using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaBase : ModelBase
{
    public enum E_LunaMovement
    {
        IDLE,
        WALK,
        PET,
        DIALOG,
        DRAG,
    }
    private E_LunaMovement lunaMovement;
}
