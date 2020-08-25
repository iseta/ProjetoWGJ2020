using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextManager : MonoBehaviour
{
    [TextArea]
    public string[] lines;
    public UnityEvent[] events;
    public TextMeshProUGUI textComponent;
    public float speed; //Velocidade entre caracteres;

    public int currentLine = 0;

    private void Start()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        StartCoroutine(TypeWriter(lines[currentLine]));
    }

    public void CheckClick()
    {
        if (currentLine < lines.Length)
        {
            if (textComponent.text != lines[currentLine])
            {
                StopAllCoroutines();
                textComponent.text = lines[currentLine];
            }
            else if(currentLine < lines.Length -1)
            {
                currentLine++;
                StartCoroutine(TypeWriter(lines[currentLine]));
                if (events[currentLine] != null)
                {
                    events[currentLine].Invoke();
                }
            }
        }
    }

    IEnumerator TypeWriter(string line)
    {
        textComponent.text = "";
        char[] chars = line.ToCharArray();
        foreach(char c in chars)
        {
            yield return new WaitForSeconds(speed);
            textComponent.text += c;
        }
    }
}
