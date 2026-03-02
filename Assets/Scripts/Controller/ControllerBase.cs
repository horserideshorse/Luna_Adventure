using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ControllerBase : MonoBehaviour
{
    protected float moveSpeed;
    protected Vector3 position;
    protected Vector3 direction;
    protected Collider2D collider2d;
    protected Rigidbody2D rigidbody2d;

    [Header("UI")]
    [SerializeField] protected Animator animator;

    protected virtual void Awake(){
        collider2d = GetComponent<Collider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>(); 
    }

    protected virtual void Start(){}

    protected virtual void FixedUpdate() { }
    protected virtual void Update() { }

    protected void SetDir(Vector3 direction)
    {
        animator.SetFloat("LookX", direction.x);
        animator.SetFloat("LookY", direction.y);
    }
    protected int RandomInt(int a, int b)
    {
        return UnityEngine.Random.Range(a, b + 1);
    }
}
