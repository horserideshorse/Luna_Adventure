using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerBase<UIManager>
{
    public GameObject talkPanle;
    public Text textName;
    public Text textContent;
    public Image charcaterSprite;
    public Sprite[] characterSprites;
    //public SpriteRenderer spriteRenderer;



    public void ShowDialog(string name = "???", string content = null)
    {
        if(content == null)
        {
            talkPanle.SetActive(false);
        }
        else
        {
            talkPanle.SetActive(true);

            int showSprite = name switch
            {
                "Luna" => 0,
                "Nala" => 1,
                "Dog" => 2,
                _ => 0
            }; charcaterSprite.sprite = characterSprites[showSprite];

            charcaterSprite.SetNativeSize();
            textName.text = name;
            textContent.text = content;
        }
    }
}
