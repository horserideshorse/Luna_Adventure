using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : ManagerBase<TalkManager>
{
    private DialogBase dialog;
    private int diaIndex;
    private int conIndex;
    private List<DialogBase.DialogInfo[]> npcDialogs;
    public void Talk()
    {
        Debug.Log("Talking");
    }

    public void DisPlayDialog(DialogBase dialogBase)
    {
        dialog = dialogBase;
        diaIndex = dialogBase.DiaIndex;
        conIndex = dialogBase.ConIndex;
        npcDialogs = dialogBase.NpcDia;

        if (diaIndex > 2) return;
        if (conIndex >= npcDialogs[diaIndex].Length)
        {
            conIndex = 0;
            diaIndex++;
            UIManager.Instance.ShowDialog();
        }
        else
        {
            conIndex++;
        }
    }
}
