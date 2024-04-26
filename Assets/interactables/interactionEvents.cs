using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class interactionEvents
{
    static interactionEvents single;

    public dialhandler DialogueHandler; //only close/open through respective handler

    public UnityEvent dialogueOpened = new UnityEvent();
    public UnityEvent closingDialogue = new UnityEvent();
    public UnityEvent bedMenuOpened = new UnityEvent();
    public UnityEvent bedMenuClosed = new UnityEvent();
    public UnityEvent triggerListUpdate = new UnityEvent();
    public UnityEvent doorEventTriggered = new UnityEvent();
    public UnityEvent fadeInDone = new UnityEvent();
    
    public static interactionEvents getSingle()
    {
        if(single == null)
            single = new interactionEvents();
        return single;
    }

    public static void registerDialogueHandler(dialhandler dh)
    {
        getSingle().DialogueHandler = dh;
    }
}
