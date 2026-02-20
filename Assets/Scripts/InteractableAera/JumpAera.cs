using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpAera : InteracterableArea
{
    public Transform jumpPointA;
    public Transform jumpPointB;
    protected override void PerformEffect()
    {
        lunaController.Jump(true);
        float disA = Vector3.Distance(lunaController.transform.position, jumpPointA.position);
        float disB = Vector3.Distance(lunaController.transform.position, jumpPointB.position);
        //跳距离远的点
        Transform targetTrans = disA > disB ? jumpPointA : jumpPointB;
        //DOTWEEN移动
        lunaController.transform.DOMove(targetTrans.position, 0.5f).SetEase(Ease.Linear)//线性动画
            .OnComplete(() => { AfterInteract(); });//播放结束回调函数
        Transform lunaSpriteTrans = lunaController.transform.GetChild(0);//LunaSprite
        //动画队列
        Sequence sequence = DOTween.Sequence();
        sequence.Append(lunaSpriteTrans.DOLocalMoveY(1.5f, 0.25f).SetEase(Ease.InOutSine));
        sequence.Append(lunaSpriteTrans.DOLocalMoveY(0.61f, 0.25f).SetEase(Ease.InOutSine));
        sequence.Play();
    }
    protected override void AfterInteract()
    {
        lunaController.Jump(false);
    }
}
