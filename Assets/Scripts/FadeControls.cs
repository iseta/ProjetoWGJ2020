using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeControls : MonoBehaviour
{
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;
    public int sceneToLoadWhenDone;
    public bool needsIntro;

    // Start is called before the first frame update
    void Start()
    {
        if (needsIntro)
        {
            StartCoroutine(Fade(-1));
        }
    }

    public void ExitOutro()
    {
        StartCoroutine(Fade(1));
    }

    private IEnumerator Fade(int dir)
    {
        float alpha = 1;
        float fadeEndValue = 0;
        while (alpha >= fadeEndValue)
        {
            SetColorImage(ref alpha, dir);
            yield return null;
            if (dir == 1)
            {
                SceneManager.LoadScene(sceneToLoadWhenDone);
            }
        }
    }

    private void SetColorImage(ref float alpha, int fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * (fadeDirection);
    }
}
