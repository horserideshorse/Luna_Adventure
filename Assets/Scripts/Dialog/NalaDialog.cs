using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NalaDialog : DialogBase
{
    private bool isPetDog;
    protected void Awake()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]{
                new() {name = "Luna", content = "剧情0-0"}  //第1句
            },  //第0段
            new DialogInfo[]{
                new() {name = "Luna", content = "剧情1-0"}, //第1句
                new() {name = "Luna", content = "剧情1-1"}, //第2句
                new() {name = "Nala", content = "剧情1-2"}, //第3句
                new() {name = "Luna", content = "剧情1-3"}, //第4句
                new() {name = "Nala", content = "剧情1-4"}, //第5句
            },  //第1段
            new DialogInfo[]{
                new() {name = "Nala", content = "接任务1-0"},
                new() {name = "Luna", content = "接任务1-1"}
            },  //第2段
            new DialogInfo[]{
                new() {name = "Nala", content = "完成了吗"},
                new() {name = "Luna", content = "未完成任务1-1"}
            },  //第3段
            new DialogInfo[]{
                new() {name = "Luna", content = "任务完成1-0"}, //第1句
                new() {name = "Luna", content = "任务完成1-1"}, //第2句
                new() {name = "Nala", content = "任务完成1-2"}, //第3句
                new() {name = "Luna", content = "任务完成1-3"}, //第4句
                new() {name = "Nala", content = "任务完成1-4"}, //第5句
            }   //第4段
        };
        _dialogIndex = 0;
        _contentIndex = 0;
    }

    public override void ContinueDialogConditions()
    {
        if (ConIndex + 1 >= NpcDialog[DiaIndex].Length)
        {
            if (DiaIndex < 2)
            {
                InterSelf = (int)LunaController.E_LunaMovement.LOOK;
                DiaIndex += 1;
                ConIndex = 0;
            }
            else if (DiaIndex == 2)
            {
                MissionManager.Instance.AddMissions = new Mission("PetDog", "Go to pet Nala's dog");

                DiaIndex = 3;
                ConIndex = -1;
            }
            else if (!MissionManager.Instance.ReadMissions[0].isComplete && DiaIndex == 3)
            {
                DiaIndex = 3;
                ConIndex = -1;
            }
            else if (MissionManager.Instance.ReadMissions[0].isComplete && DiaIndex == 4)
            {
                DiaIndex = -1;
                ConIndex = -1;
            }
        }
        else
        {
            if (MissionManager.Instance.ReadMissions.Count != 0)
            {
                if (MissionManager.Instance.ReadMissions[0].isComplete && DiaIndex ==3)
                {
                    DiaIndex = 4;
                    ConIndex = 0;
                }
            }
            ConIndex += 1;
        }
    }
}
