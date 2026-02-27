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
            _dialogBase.OnInterSelfChange -= OnInterSelfChange;
            _dialogBase.OnInterTargetChange -= OnInterTargetChange;
        }

        _dialogBase = dialog;
        _npcDialogs = _dialogBase.NpcDialog;
        _dialogBase.OnContentChange += OnContentChange;
        _dialogBase.OnInterSelfChange += OnInterSelfChange;
        _dialogBase.OnInterTargetChange += OnInterTargetChange;

        OnContentChange(_dialogBase.ConIndex);
    }
    public void ExitDialog(DialogBase dialog)
    {
        if (_dialogBase != null)
        {
            _dialogBase.OnContentChange -= OnContentChange;
            _dialogBase.OnInterSelfChange -= OnInterSelfChange;
            _dialogBase.OnInterTargetChange -= OnInterTargetChange;
        }
    }

    private void OnContentChange(int temp){
        _diaIndex = _dialogBase.DiaIndex;
        _conIndex = _dialogBase.ConIndex;
        DisPlayDialog();
    }
    private void OnInterSelfChange(int interacteState)
    {
        _lunaController.LunaMovement = (LunaController.E_LunaMovement)interacteState;
    }

    private void OnInterTargetChange(int temp)
    {
        if(_dialogBase.gameObject.GetComponent<CharacterBase>())
        _dialogBase.gameObject.GetComponent<CharacterBase>().InteracteAni(temp);
    }

    public void DisPlayDialog()
    {
        if (_dialogBase.ConIndex == -1){
            UIManager.Instance.ShowDialog();
            Instance.ExitDialog(_dialogBase);
            _lunaController.IsDialog = false;
        }
        else {
            DialogInfo dialog = _npcDialogs[_diaIndex][_conIndex];
            UIManager.Instance.ShowDialog(dialog.name, dialog.content);
            _lunaController.IsDialog = true;
        } 
    }
}
