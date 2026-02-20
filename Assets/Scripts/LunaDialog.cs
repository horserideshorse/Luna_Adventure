using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaDialog : DialogBase
{
    protected void Awake()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]{
                new() {name = "Luna", content = "教程0-0"}, //第1句
                new() {name = "Luna", content = "教程0-1"}, //第2句
                new() {name = "Luna", content = "教程0-2"}, //第3句
                new() {name = "Luna", content = "教程0-3"}, //第4句
                new() {name = "Luna", content = "教程0-4"}, //第5句
            } //第1段
        };
        _dialogIndex = 0;
        _contentIndex = 0;
        Debug.Log(NpcDialog);
    }
}
