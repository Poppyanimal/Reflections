using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayThenDecay : MonoBehaviour
{
    AudioSource player;

    public void initThenPlay(AudioClip clip) { initThenPlay(clip, 1f, 1f); }
    public void initThenPlay(AudioClip clip, float vol) { initThenPlay(clip, vol, 1f); }
    public void initThenPlay(AudioClip clip, float vol, float decayDelay)
    {
        player = gameObject.GetComponent<AudioSource>();
        player.clip = clip;
        player.volume = vol;
        float delay = clip.length + decayDelay;
        
        player.Play();
        StartCoroutine(decay(delay));
    }

    IEnumerator decay(float decayTime)
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(this.gameObject);
    }
}
