using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaDialog : DialogBase
{
    protected void Awake()
    {
        dialogInfoList = new string[]
        {
            new("哈喽"), //第1句
            new("你好"), //第2句
            new("过的怎么样"), //第3句
            new("今天天气真好"), //第1句
            new("(•_•))"), //第2句
            new("`*>__<*′") //第3句
        };
    }
}
