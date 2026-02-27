using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DogController;
using static LunaController;

public class DogController : CharacterBase
{
    public enum E_DogState
    {
        IDLE,
        PET,
        BARK
    }
    private E_DogState dogState;

    public override void InteracteAni(int _isInteracte = 0)
    {
        dogState =_isInteracte switch
        {
            0 => E_DogState.IDLE,
            1 => E_DogState.PET,
            2 => E_DogState.BARK,
            _ => E_DogState.IDLE
        }; animatorOutBattle.SetInteger("dogeState", _isInteracte);
    }
}
