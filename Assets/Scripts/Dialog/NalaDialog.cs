using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NalaDialog : DialogBase
{
    //private bool isTrue;
    protected void Awake()
    {
        //isTrue = false;
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]{
                new() {name = "Luna", content = "剧情0-0"}  //第1句
            }, //第1段
            new DialogInfo[]{
                new() {name = "Luna", content = "剧情1-0"}, //第1句
                new() {name = "Luna", content = "剧情1-1"}, //第2句
                new() {name = "Nala", content = "剧情1-2"}, //第3句
                new() {name = "Luna", content = "剧情1-3"}, //第4句
                new() {name = "Nala", content = "剧情1-4"}, //第5句
            }, //第2段
            new DialogInfo[]{
                new() {name = "Nala", content = "剧情2-0"},
                new() {name = "Luna", content = "剧情2-1"}
            }  //第3段
        };
        _dialogIndex = 0;
        _contentIndex = 0;
    }

    //public override void ContinueDialogConditions()
    //{
    //    if(!isTrue && DiaIndex == 1 && ConIndex == 4)
    //    {

    //    }
    //}
}
