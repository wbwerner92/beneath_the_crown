using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTextManager : MonoBehaviour
{
    public static MainTextManager instance;

    public Text mainText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }
    public void ShowObject(bool clearText = false)
    {
        if (clearText)
        {
            ClearMainText();
        }
        gameObject.SetActive(true);
    }

    public void ClearMainText()
    {
        mainText.text = "";
    }
    public void AppendMainText(string t)
    {
        Debug.Log(t);
        string newTextAppendage = (mainText.text == "") ? "" : "\n\n";
        mainText.text = mainText.text + newTextAppendage + t;
    }
}
