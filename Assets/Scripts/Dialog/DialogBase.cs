using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogInfo
{
    public string name;
    public string content;
}
public class DialogBase : MonoBehaviour
{
    protected List<DialogInfo[]> dialogInfoList; //ÄÚÈÝ
    protected int _dialogIndex;                   //¶ÎÂä
    protected int _contentIndex;                  //¾äÂä

    protected GameObject pressTip;

    public event Action<int> OnContentChange;

    public List<DialogInfo[]> NpcDialog { get => dialogInfoList; }
    public int DiaIndex 
    {
        get => _dialogIndex;
        private set{
            _dialogIndex = value;
        }
    }
    public int ConIndex 
    { 
        get => _contentIndex;
        private set
        {
            _contentIndex = value;
            OnContentChange?.Invoke(_contentIndex);
        }
    }
    public GameObject PressTip { get => pressTip; }

    protected virtual void Start()
    {
        if (gameObject.GetComponentInChildren<Canvas>()){
            pressTip = gameObject.GetComponentInChildren<Canvas>().gameObject;
            UIManager.Instance.ShowOrHidePressTip(this);
        }
    }

    public virtual void ContinueDialogConditions()
    {
        if (ConIndex + 1 >= NpcDialog[DiaIndex].Length)
        {
            if (DiaIndex + 1 < NpcDialog.Count)
            {
                DiaIndex += 1;
                ConIndex = 0;
            }
            else
            {
                DiaIndex = -1;
                ConIndex = 0;
            }
        }
        else
        {
            ConIndex += 1;
        }
    }
}
