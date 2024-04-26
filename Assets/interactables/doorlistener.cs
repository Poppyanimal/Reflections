using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorlistener : MonoBehaviour
{
    public List<GameObject> dialoguesToDisableOnDoorEvent = new List<GameObject>();
    public string newLine = "I just want to lay down...";
    void Start()
    {
        interactionEvents.getSingle().doorEventTriggered.AddListener(triggerWipe);
    }

    void triggerWipe()
    {
        foreach(GameObject g in dialoguesToDisableOnDoorEvent)
        {
            //g.SetActive(false);

            g.SetActive(true);

            dialogueinteractable i = g.GetComponent<dialogueinteractable>();
            i.disableOnDialogueEnd = false;
            i.dialogues = new List<string>() {newLine};
            i.resetIndex();
        }
    }
}
