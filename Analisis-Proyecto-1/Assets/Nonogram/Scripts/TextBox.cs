using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using CodeMonkey;
using TMPro;
using System;

public class TextBox : MonoBehaviour
{
    private Button_UI okBtn;
    private TMP_InputField inputField;

    private void Awake(){
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        Hide();
    }

    public void Show(Action<string> onOk){
        gameObject.SetActive(true);

        okBtn.ClickFunc = () => {
            Hide();
            onOk(inputField.text);
        };
    }
    
    public void Hide(){
        gameObject.SetActive(false);
    }
}
