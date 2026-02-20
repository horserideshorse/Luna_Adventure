using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSample : MonoBehaviour
{
    public Animator animator;
    void Start()
    {

        //启动协程两种方式
        StartCoroutine(ChangeState());
        StartCoroutine("ChangeState");

        //停止协程两种方式（只能用对应的来结束）
        StopCoroutine(ChangeState());
        StopCoroutine("ChangeState");

        //一般形式
        IEnumerator ie = ChangeState();
        StartCoroutine(ie);
        StopCoroutine(ie);

        //停止所有
        StopAllCoroutines();
    }
    void Update()
    {
        
    }
    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(2); //协程挂起2秒
        animator.Play("Walk");
        yield return null; //挂起1帧
        yield return 10000; //挂起1帧
        yield return new WaitForEndOfFrame(); //本帧末执行
    }
}
