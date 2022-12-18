using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource targetAudioSource;
    [Range(0, 1)] public float volume = 1;

    public void PlayClip()
    {
        if (clip == null)
        {
            Debug.LogError("Missing clip from:" + this.gameObject.name);
            return;
        }

        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clip);
    }

    public void PlaySpecificClip(AudioClip clipToPlay = null)
    {
        if (clipToPlay == null)
        {
            clipToPlay = clip;
        }

        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
