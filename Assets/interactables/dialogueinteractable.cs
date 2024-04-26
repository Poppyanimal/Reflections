using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueinteractable : MonoBehaviour, interactable
{
    public bool disableOnDialogueEnd = false; //repeats last dialogue elsewise
    public bool triggerDoorEvent = false;
    public int indexToTriggerDoorEvent  = 0;
    public List<string> dialogues = new List<string>() {"This is a placeholder text..."};
    int currentIndex = 0;
    public void interact()
    {
        if(currentIndex >= dialogues.Count)
        {
            interactionEvents.getSingle().DialogueHandler.openText("Error: this code should be unreachable\nCheck if interactable has lines.");
            disableSelf();
            return;
        }

        if(triggerDoorEvent && indexToTriggerDoorEvent == currentIndex)
            interactionEvents.getSingle().doorEventTriggered.Invoke();

        interactionEvents.getSingle().DialogueHandler.openText(dialogues[currentIndex]);
        currentIndex++;
        if(currentIndex >= dialogues.Count)
        {
            if(disableOnDialogueEnd)
                disableSelf();
            else
                currentIndex = dialogues.Count - 1;
        }
    }

    void disableSelf()
    {
        player.removeTriggerFromList(gameObject.GetComponent<Collider>());
        this.gameObject.SetActive(false);
    }

    public void resetIndex() { currentIndex = 0; }
}
