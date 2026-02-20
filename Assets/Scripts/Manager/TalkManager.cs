using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : ManagerBase<TalkManager>
{
    private DialogBase _dialogBase;
    private List<DialogInfo[]> _npcDialogs;
    private int _diaIndex;
    private int _conIndex;

    public DialogBase Dialog { set { _dialogBase = value; } }
    public void LoadDialog(DialogBase dialog)
    {
        if (_dialogBase != null){
            
            _dialogBase.OnContentChange -= OnContentChange;
        }

        _dialogBase = dialog;
        _npcDialogs = _dialogBase.NpcDialog;
        _dialogBase.OnContentChange += OnContentChange;
        
        OnContentChange(_dialogBase.ConIndex);
    }
    private void OnContentChange(int temp){
        _diaIndex = _dialogBase.DiaIndex;
        _conIndex = _dialogBase.ConIndex;
        DisPlayDialog();
    }
    public void DisPlayDialog()
    {
        if (_dialogBase.DiaIndex == -1){
            UIManager.Instance.ShowDialog();
        }
        else {
            
            DialogInfo dialog = _npcDialogs[_diaIndex][_conIndex];
            UIManager.Instance.ShowDialog(dialog.name, dialog.content);
        } 
    }
}
