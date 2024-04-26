using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dialhandler : MonoBehaviour
{
    public TMPro.TMP_Text textDisplay;
    public Canvas canvas;
    void Start()
    {
        interactionEvents.registerDialogueHandler(this);
    }
    public void closeCanvas()
    {
        canvas.gameObject.SetActive(false);
        interactionEvents.getSingle().closingDialogue.Invoke();
    }

    public void openText(string t)
    {
        string newText = t;
        newText = newText.Replace("\\n", Environment.NewLine);
        textDisplay.text = newText;
        canvas.gameObject.SetActive(true);
        interactionEvents.getSingle().dialogueOpened.Invoke();
    }
}
