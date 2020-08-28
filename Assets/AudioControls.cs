using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControls : MonoBehaviour
{
    public AudioSource self;

    public void AdjustVolume(bool i)
    {
        if (i)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        float volume = 0.5f;
        float volumeEndValue = 0;
        while (volume >= volumeEndValue)
        {
            SetSoundVolume(ref volume, -1);
            yield return null;
        }
        self.volume = volumeEndValue;
    }

    private IEnumerator FadeOut()
    {
        float volume = 0;
        float volumeEndValue = 0.5f;
        while (volume <= volumeEndValue)
        {
            SetSoundVolume(ref volume, 1);
            yield return null;
        }
        self.volume = volumeEndValue;
    }

    private void SetSoundVolume(ref float volume, int Dir)
    {
        self.volume = volume;
        volume += Time.deltaTime * (1.0f / 1.0f) * (Dir);
    }
}
