using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadehandler : MonoBehaviour
{
    public static fadehandler single;
    public float fadeInTime = 3f;
    public float fadeOutTime = 5f;
    public float startWaitTime = 1f;
    public float finishWaitTime = 1f;
    public bool playIntro = true;
    public Image fader;

    void Start()
    {
        AudioListener.volume = 0f;
        single = this;
        if(playIntro)
        {
            StartCoroutine(fadeIn());
        }
        else
        {
            AudioListener.volume = 1f;
            Color temp = fader.color;
            temp.a = 0f;
            fader.color = temp;
            interactionEvents.getSingle().fadeInDone.Invoke();
        }
    }

    IEnumerator fadeIn()
    {
        yield return new WaitForSeconds(startWaitTime);

        float startTime = Time.time;
        yield return new WaitUntil(delegate()
        {
            float timeDif = Time.time - startTime;
            if(timeDif >= fadeInTime)
            {
                Color temp = fader.color;
                temp.a = 0f;
                fader.color = temp;
                AudioListener.volume = 1f;
                return true;
            }
            else
            {
                Color temp = fader.color;
                temp.a = 1f - (timeDif/fadeInTime);
                AudioListener.volume = timeDif/fadeInTime;
                fader.color = temp;
                return false;
            }
        });

        interactionEvents.getSingle().fadeInDone.Invoke();
    }

    public void startEndFade()
    {
        StartCoroutine(endFade());
    }

    
    IEnumerator endFade()
    {
        float startTime = Time.time;
        yield return new WaitUntil(delegate()
        {
            float timeDif = Time.time - startTime;
            if(timeDif >= fadeOutTime)
            {
                Color temp = fader.color;
                temp.a = 1f;
                fader.color = temp;
                AudioListener.volume = 0f;
                return true;
            }
            else
            {
                Color temp = fader.color;
                temp.a = timeDif/fadeOutTime;
                fader.color = temp;
                AudioListener.volume = 1f - (timeDif/fadeOutTime);
                return false;
            }
        });

        Application.Quit();
    }

}
