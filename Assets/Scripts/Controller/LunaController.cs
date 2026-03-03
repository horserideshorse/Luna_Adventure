using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class LunaController : ControllerBase
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

    private float timer;

    private bool isDialog;     //正在对话
    private bool isDrag;
    private bool isPet;

    private SpriteRenderer dog;
    public TextMeshProUGUI text;

    private DialogBase dialog;

    public E_LunaMovement LunaMovement { get => lunaMovement; set { lunaMovement = value; } }
    public bool IsDialog { get => isDialog; set { isDialog = value; } }

    protected override void Start()
    {
        dog = gameObject.transform.Find("$Dog_0").GetComponent<SpriteRenderer>();
        dialog = gameObject.GetComponent<DialogBase>();
        //TalkManager.Instance.LoadDialog(dialog);
        MouseManager.Instance.MouseKeyDownEvent += MouseKeyDownEvent;
        MouseManager.Instance.MouseKeyUpEvent += MouseKeyUpEvent;
        MouseManager.Instance.MouseDragEvent += MouseDragEvent;
        MouseManager.Instance.MouseKeyClickEvent += MouseKeyClickEvent;

        //dog.SetActive(false);
        dog.DOFade(0f, 0f);
        text.gameObject.SetActive(false);

        isDialog = false;
        isDrag = false;
        isPet = false;

        moveSpeed = 0;
        direction.x = 0;
        direction.y = -1;

        timer = 1;
    }

    protected override void Update()
    {
        if (!isDrag){
            timer -= Time.deltaTime;
            Console.WriteLine(timer);
            if (timer < 0) RandomMove();
        }
    }

    protected override void FixedUpdate()
    {
        position = rigidbody2d.position;
        position += moveSpeed * direction * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    private void MouseKeyDownEvent(MouseKey key, Vector3 mousePos)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        if (Physics2D.Raycast(worldPoint, Vector2.zero))
        {
        }
    }
    private void MouseKeyUpEvent(MouseKey key, Vector3 mousePos)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        if (Physics2D.Raycast(worldPoint, Vector2.zero))
        {
            isDrag = false;;
        }
    }
    private void MouseDragEvent(MouseKey key, Vector3 mousePos)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        if (Physics2D.Raycast(worldPoint, Vector2.zero))
        {
            
            if (key == MouseKey.Left)
            {
                if (!isDrag)
                {
                    isDrag = true;
                    RandomMove(-1);
                }
                transform.position = new Vector3(worldPoint.x, worldPoint.y - 0.5f, 0);
            }
        }
        else{ 
            isDrag = false;
            Debug.Log(isDrag);
        }
    }
    private void MouseKeyClickEvent(MouseKey key)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(MouseManager.Instance.MousePos);
        if (Physics2D.Raycast(worldPoint, Vector2.zero))
        {
            if (key == MouseKey.Left && !isPet)
            {
                isPet = true;
                RandomMove(3);
                dog.DOFade(1f, 0.2f).OnComplete(() => { StartCoroutine(PetDog()); });
            }
            else if(key == MouseKey.Right && !isDialog)
            {
                isDialog = true;
                text.gameObject.SetActive(true);
                StartCoroutine(Dialog());
            }
        }
    }
    private IEnumerator Dialog()
    {
        yield return new WaitForSecondsRealtime(2f);
        text.text = dialog.NpcDialog[RandomInt(0, dialog.NpcDialog.Length - 1)];
        text.gameObject.SetActive(false);
        isDialog = false;
    }

    private IEnumerator PetDog()
    {
        yield return new WaitForSecondsRealtime(2.7f);
        isPet = false;
        dog.DOFade(0f, 0.2f);
    }

    private void RandomMove(int value = 0)
    {
        timer = value != 0 ? value : RandomInt(1, 2);

        moveSpeed = value != 0 ? 0 : RandomInt(0, 1);
        direction.x = value != 0 ? 0 : RandomInt(-1, 1);
        direction.y = value != 0 ? -1 : RandomInt(-1, 1);

        SetDir(direction);

        ChangeMoveWay();
    }

    private void ChangeMoveWay()
    {
        if (isDrag && !isPet)
        {
            MoveAni(E_LunaMovement.DRAG);
        }
        else if (isPet && !isDrag)
        {
            MoveAni(E_LunaMovement.PET);
        }
        else if (moveSpeed == 0) {
            MoveAni(E_LunaMovement.IDLE);
            SetDir(new Vector3(0, -1));
        }
        else if (moveSpeed != 0) {
            MoveAni(E_LunaMovement.WALK);
        }
    }
    private void MoveAni(E_LunaMovement movement = E_LunaMovement.IDLE)
    {
        lunaMovement = movement;
        animator.SetInteger("moveWay", (int)lunaMovement);
    }
}