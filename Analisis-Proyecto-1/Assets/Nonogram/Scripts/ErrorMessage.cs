using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using CodeMonkey;
using TMPro;
using System;

public class ErrorMessage : MonoBehaviour
{
    private Button_UI okBtn;

    private void Awake(){
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        Hide();
    }

    public void Show(){
        gameObject.SetActive(true);

        okBtn.ClickFunc = () => {
            Hide();
            UnityEditor.EditorApplication.isPlaying = false;
            //System.Environment.Exit(0);esto crashea unity
        };
    }
    
    private void Hide(){
        gameObject.SetActive(false);
    }
}