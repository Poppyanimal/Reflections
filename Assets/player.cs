using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public static List<Collider> triggerList = new List<Collider>();
    
    Rigidbody playerBody;
    Collider playerCol;
    public GameObject maincam;
    public GameObject interactNotif;
    public GameObject sanroot;
    public Animator sanaanim;
    public float movementSpeed = 1f;
    public float maxRotateSpeed = 5f;
    public bool doRot = true;
    float deadzone = .01f;
    float targetAngle;

    bool dialogueLocked = false;
    bool bedLocked = false;
    bool fadeLocked = true;

    bool justClosedMenu = false;
    bool ignoredFirstInteract = false;

    void Start()
    {
        targetAngle = sanroot.transform.rotation.eulerAngles.y;
        Cursor.visible = false;
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCol = gameObject.GetComponent<Collider>();
        //Coroutine turnCoro = StartCoroutine(rotatePlayerSmooth());
        interactionEvents.getSingle().dialogueOpened.AddListener(tripDialogue);
        interactionEvents.getSingle().closingDialogue.AddListener(tripDialogueClose);
        interactionEvents.getSingle().bedMenuOpened.AddListener(tripBed);
        interactionEvents.getSingle().bedMenuClosed.AddListener(tripBedClose);
        interactionEvents.getSingle().triggerListUpdate.AddListener(tripTriggerListUpdate);
        interactionEvents.getSingle().fadeInDone.AddListener(fadedIn);
    }

    void Update()
    {
        if(fadeLocked)
        {
            return;
        }
        else if(dialogueLocked)
        {
            playerBody.velocity = Vector3.zero;
            sanaanim.SetBool("isMoving", false);

            if(Input.GetButtonDown("Submit"))
                interactionEvents.getSingle().DialogueHandler.closeCanvas();
        }
        else if(bedLocked)
        {
            //TODO, wait on bed menu to unlock self, no code needed here
            playerBody.velocity = Vector3.zero;
            sanaanim.SetBool("isMoving", false);
        }
        else
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 inputdir = input.normalized;
            Vector2 movementdir = skewByCamera(inputdir).normalized;

            bool moving = input.magnitude > deadzone;
            sanaanim.SetBool("isMoving", moving);
            if(moving) 
                updateTargetAngle(movementdir);

            playerBody.velocity = new Vector3(movementdir.x, 0f, movementdir.y)  * movementSpeed;

            if(justClosedMenu)
            {
                justClosedMenu = false;
                return;
            }

            if(triggerList.Count > 0 && Input.GetButtonDown("Submit"))
            {
                triggerList[0].gameObject.GetComponent<interactable>().interact();
            }
        }
    }

    public void fadedIn() { fadeLocked = false; }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Trigger Enter Called");

        if(ignoredFirstInteract)
            addTriggerToList(c);
        else
            ignoredFirstInteract = true;
    }

    void OnTriggerExit(Collider c)
    {
        //Debug.Log("Trigger Exit Called");
        removeTriggerFromList(c);
    }

    void updateInteractionMarker(bool state)
    {
        interactNotif.SetActive(state);
    }

    public static void removeTriggerFromList(Collider c)
    {
        if(triggerList.Contains(c))
            triggerList.Remove(c);
        interactionEvents.getSingle().triggerListUpdate.Invoke();
    }

    public static void addTriggerToList(Collider c)
    {
        if(!triggerList.Contains(c))
            triggerList.Add(c);
        interactionEvents.getSingle().triggerListUpdate.Invoke();
    }

    void tripTriggerListUpdate()
    {
        bool hasTrigger = triggerList.Count > 0;
        updateInteractionMarker(hasTrigger);
    }

    void tripBedClose()
    {
        bedLocked = false;
        tripTriggerListUpdate();
        justClosedMenu = true;
    }
    void tripBed()
    {
        bedLocked = true;
        interactNotif.SetActive(false);
    }

    void tripDialogueClose()
    {
        dialogueLocked = false;
        tripTriggerListUpdate();
        justClosedMenu = true;
    }
    void tripDialogue()
    {
        dialogueLocked = true;
        interactNotif.SetActive(false);
    }

    Vector2 skewByCamera(Vector2 inputs)
    {
        float camAngle = 360f - maincam.transform.eulerAngles.y;
        return KiroLib.rotateVector2eul(camAngle, inputs);
    }

    void updateTargetAngle(Vector2 dir)
    {
        float newAngle = 180f - KiroLib.angleToTarget(Vector2.up, dir) * 2f;
        targetAngle = KiroLib.normalizeEuler(newAngle);
    }

    void FixedUpdate()
    {
        if(doRot)
        {
            Vector3 sanrot = sanroot.transform.rotation.eulerAngles;
            float curAngle = KiroLib.normalizeEuler(sanrot.y);
            float dif = targetAngle - curAngle; 

            if(dif > 180f)
                dif -= 360f;
            else if(dif < -180f)
                dif+= 360f;

            if(dif > maxRotateSpeed)
                dif = maxRotateSpeed;
            else if(dif < -maxRotateSpeed)
                dif = -maxRotateSpeed;

            sanrot.y = curAngle + dif;
            sanroot.transform.rotation = Quaternion.Euler(sanrot);
        }
    }

    /*IEnumerator rotatePlayerSmooth()
    {
        yield return new WaitUntil(delegate()
        {
            if(!doRot)
                return false;
            Vector3 sanrot = sanroot.transform.rotation.eulerAngles;
            float curAngle = KiroLib.normalizeEuler(sanrot.y);
            float dif = targetAngle - curAngle; 

            if(dif > 180f)
                dif -= 360f;
            else if(dif < -180f)
                dif+= 360f;

            if(dif > maxRotateSpeed)
                dif = maxRotateSpeed;
            else if(dif < -maxRotateSpeed)
                dif = -maxRotateSpeed;

            sanrot.y = curAngle + dif;
            sanroot.transform.rotation = Quaternion.Euler(sanrot);
            return false;
        });


    }*/

}
