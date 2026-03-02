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
    protected List<DialogInfo[]> dialogInfoList;  //内容
    protected int _dialogIndex;                   //段落
    protected int _contentIndex;                  //句落
    protected int _interacteSelf;                 //交互方式自己
    protected int _interacteTarget;               //交互方式对方
    protected GameObject pressTip;                //按键提示

    public event Action<int> OnContentChange;
    public List<DialogInfo[]> NpcDialog { get => dialogInfoList; }
    
    public int DiaIndex 
    {
        get => _dialogIndex;
        set{
            _dialogIndex = value;
        }
    }
    public int ConIndex 
    { 
        get => _contentIndex;
        set{
            _contentIndex = value;
            OnContentChange?.Invoke(_contentIndex);
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
                DiaIndex = 0;
                ConIndex = -1;
            }
        }
        else
        {
            ConIndex += 1;
        }
    }
}
