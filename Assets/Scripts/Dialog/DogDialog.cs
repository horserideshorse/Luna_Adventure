using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDialog : DialogBase
{
    protected void Awake()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]{
                new() {name = "Luna", content = "......"}  //第1句
            },  //第1段
            new DialogInfo[]{
                new() {name = "Luna", content = "摸摸"},  //第1句
                new() {name = "Dog", content = "汪！"} //第2句
            },  //第2段
            new DialogInfo[]{
                new() {name = "Luna", content = "摸摸中。。。"}  //第1句
            }  //第2段
        };
        _dialogIndex = -1;
        _contentIndex = 0;
    }

    protected override void Start()
    {
        base.Start();

        if (MissionManager.Instance.ReadMissions != null)
        {
            MissionManager.Instance.OnMissionsListChange += OnMissionsListChange;
        }
    }

    private void OnMissionsListChange(int missions)
    {
        DiaIndex = 0;
    }

    public override void ContinueDialogConditions()
    {
        if (ConIndex + 1 >= NpcDialog[DiaIndex].Length)
        {
            if (DiaIndex == 0)
            {
                InterSelf = (int)LunaController.E_LunaMovement.LOOK;
                InterTarget = (int)DogController.E_DogState.BARK;
                DiaIndex += 1;
                ConIndex = 0;
            }
            else if (DiaIndex == 1)
            {
                InterSelf = (int)LunaController.E_LunaMovement.PET;
                InterTarget = (int)DogController.E_DogState.PET;
                DiaIndex += 1;
                ConIndex = 0;
            }
            else if (DiaIndex == 2)
            {
                MissionManager.Instance.ReadMissions[0].IfMissionComplete();
                MissionManager.Instance.OnMissionsListChange -= OnMissionsListChange;
                InterTarget = (int)DogController.E_DogState.IDLE;
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
