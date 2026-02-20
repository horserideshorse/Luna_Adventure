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

    public void SetHpValue(ControllerBase controller)
    {
        controller.HpMask.rectTransform.SetSizeWithCurrentAnchors(
    RectTransform.Axis.Horizontal, controller.HP * 0.2f * controller.HpWidth);

        if (controller is LunaController)
        {
            lunaController = controller as LunaController;
            lunaController.hpMaskMap.rectTransform.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal, controller.HP * 0.2f * lunaController.HpWidthMap);
            lunaController.text_HP_value.text = $"{lunaController.HP}/{lunaController.MaxHP}";
        }
    }

    public void SetMpValue(ControllerBase controller)
    {
        controller.MpMask.rectTransform.SetSizeWithCurrentAnchors(
    RectTransform.Axis.Horizontal, controller.MP * 0.2f * controller.MpWidth);

        if (controller is LunaController)
        {
            lunaController = controller as LunaController;
            lunaController.mpMaskMap.rectTransform.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal, controller.MP * 0.2f * lunaController.MpWidthMap);
            lunaController.text_MP_value.text = $"{lunaController.MP}/{lunaController.MaxMP}";
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

    public void ShowDialog(string content = null, string name = null)
    {

    }
}
