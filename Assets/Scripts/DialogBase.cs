using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogBase : MonoBehaviour
{
    public class DialogInfo{
        public string name;
        public string content;
    }

    protected List<DialogInfo[]> npcDialogs; //ÄÚÈÝ
    protected int dialogIndex;               //¶ÎÂä
    protected int contentIndex;              //¾äÂä

    public List<DialogInfo[]> NpcDia { get { return npcDialogs; } }
    public int DiaIndex { get { return dialogIndex; } }
    public int ConIndex { get { return contentIndex; } }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        TalkManager.Instance.DisPlayDialog(this);
    }
}
