using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogoPorLinhas : MonoBehaviour
{
    int currentLine;
    TextMeshProUGUI textComponent;

    [TextArea]
    public String txt;
    public float speed;
    public float waitWhenDone;
    public UnityEvent doWhenDone;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void StartTyping()
    {
        StartCoroutine(TypeWriter(txt));
    }

    IEnumerator TypeWriter(string line)
    {
        string[] lines = txt.Split('\n');
        textComponent.text = "";

        foreach (string s in lines)
        {
            textComponent.text += "\n" + s;
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(waitWhenDone);
        doWhenDone.Invoke();
    }
}
