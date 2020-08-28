using UnityEngine;
using System.Collections;

public class MenuAppearScript : MonoBehaviour
{
    public Animator menu; // Assign in inspector
    public AudioSource effect;
    public bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!isShowing)
            {
                ShowExitMenu();
            }
            else
            {
                HideExitMenu();
            }
        }
    }

    public void ShowExitMenu()
    {
        isShowing = true;
        menu.Play("IntroExit");
        effect.Play();
    }

    public void HideExitMenu()
    {
        isShowing = false;
        menu.Play("OutroExit");
        effect.Play();
    }
}