using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bedInteractable : MonoBehaviour, interactable
{
    public Canvas bedCanvas;
    public GameObject noButton;
    public void interact()
    {
        interactionEvents.getSingle().bedMenuOpened.Invoke();
        openCanvas();
    }

    public void selectReturn() { closeBedMenu(); }
    public void selectSleep() { gameEnd(); }

    void closeBedMenu() //called for returning to play, not for closing the game
    {
        closeCanvas();
        interactionEvents.getSingle().bedMenuClosed.Invoke();
    }

    void gameEnd()
    {
        //closeCanvas();
        //TODO: trigger ending fadeout of sound and light, then close game
        //
        //

        //Application.Quit();
        //DEBUG
        //interactionEvents.getSingle().bedMenuClosed.Invoke();

        closeCanvas();
        fadehandler.single.startEndFade();
    }

    void openCanvas()
    {
        EventSystem.current.SetSelectedGameObject(noButton);
        bedCanvas.gameObject.SetActive(true);
    }

    void closeCanvas()
    {
        EventSystem.current.SetSelectedGameObject(null);
        bedCanvas.gameObject.SetActive(false);
    }
}
