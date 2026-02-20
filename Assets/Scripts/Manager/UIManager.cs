using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerBase<UIManager>
{
    public GameObject battleGo;     //战斗背景
    public GameObject battlePanle;//战斗界面
    public GameObject mapPanle;
    public GameObject talkPanle;
    public Text textName;
    public Text textContent;
    public Image charcaterSprite;
    public Sprite[] characterSprites;
    //public SpriteRenderer spriteRenderer;
    public void SetHpValue(ControllerBase controller)
    {
        controller.HpMask.rectTransform.SetSizeWithCurrentAnchors(
    RectTransform.Axis.Horizontal, controller.HP * 0.2f * controller.HpWidth);

        if (controller is LunaController)
        {
            _lunaController = controller as LunaController;
            _lunaController.hpMaskMap.rectTransform.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal, controller.HP * 0.2f * _lunaController.HpWidthMap);
            _lunaController.text_HP_value.text = $"{_lunaController.HP}/{_lunaController.MaxHP}";
        }
    }

    public void SetMpValue(ControllerBase controller)
    {
        controller.MpMask.rectTransform.SetSizeWithCurrentAnchors(
    RectTransform.Axis.Horizontal, controller.MP * 0.2f * controller.MpWidth);

        if (controller is LunaController)
        {
            _lunaController = controller as LunaController;
            _lunaController.mpMaskMap.rectTransform.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal, controller.MP * 0.2f * _lunaController.MpWidthMap);
            _lunaController.text_MP_value.text = $"{_lunaController.MP}/{_lunaController.MaxMP}";
        }
    }

    public void ShowOrHideBattlePanle(bool show = true)
    {
        battlePanle.SetActive(show);
    }

    public void ShowOrHideBattleGo(bool show = true)
    {
        battleGo.SetActive(show);
    }
    public void ShowOrHideMapGo(bool show = true)
    {
        mapPanle.SetActive(show);
    }
    public void ShowOrHidePressTip(DialogBase dialog, bool show = false)
    {
        dialog.PressTip.SetActive(show);
    }

    public void ShowDialog(string name = "???", string content = null)
    {
        if(content == null)
        {
            _lunaController.CanControll = true;
            talkPanle.SetActive(false);
        }
        else
        {
            _lunaController.CanControll = false;
            talkPanle.SetActive(true);
            if (name == "Luna")
            {
                charcaterSprite.sprite = characterSprites[0];
            }
            else
            {
                charcaterSprite.sprite = characterSprites[1];
            }
            charcaterSprite.SetNativeSize();
            textName.text = name;
            textContent.text = content;
        }
    }
}
