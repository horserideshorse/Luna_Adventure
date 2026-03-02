using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DogController;
using static LunaController;

public class DogController : ControllerBase
{
    public enum E_DogState
    {
        IDLE,
        PET,
        BARK
    }
    private E_DogState dogState;

    public void InteracteAni(E_DogState _isInteracte)
    {
        dogState = _isInteracte;
        animator.SetInteger("dogeState", (int)dogState);
    }
}
