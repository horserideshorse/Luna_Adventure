using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected float moveSpeed;
    protected Vector3 position;
    protected Vector3 direction;
    protected Transform transformOutBattle;

    

    [Header("UI")]
    [SerializeField] protected Animator animatorOutBattle;
    protected float hpWidthInBattle;
    protected float mpWidthInBattle;

    protected virtual void Awake()
    {
        transformOutBattle = transform;
    }

    protected virtual void Start() { }
    public virtual IEnumerator Hurt() { yield return null; }
    public virtual IEnumerator Death()
    {
        Destroy(gameObject);
        DOTween.Kill(gameObject.GetComponent<SpriteRenderer>());
        yield return null;
    }

    public virtual void InteracteAni(int isInteracte) { }

    protected virtual void FixedUpdate() { }
    protected virtual void Update() { }
}
