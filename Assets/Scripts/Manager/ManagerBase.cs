using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    [SerializeField] protected LunaController _lunaController;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else Destroy(gameObject);
    }
}
