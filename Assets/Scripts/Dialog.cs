using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
   public void UpdateDialog(string title, string content) //phương thức cập nhật giao diện của Dialog
    {
        if (titleText)
            titleText.text = title;

        if (contentText)
            contentText.text = content;
    }
}
