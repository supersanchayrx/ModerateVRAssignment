using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FairyUi : MonoBehaviour
{
    public GameObject textpanel;
    public FairyPathController pathControllerScript;
    public InputActionReference inputActionFairy;

    public string[] dialogues;

    public bool DialogueCompleted, actionCompleted, next;

    public int dialgoueCount = 0;

    public float timer, speed;

    public AudioSource breathingAudio;

    public CanvasGroup canvasGroup;
    private void Start()
    {
        textpanel.SetActive(false);
        next = false;
        DialogueCompleted = false;
        actionCompleted = false;

        timer = 0;

        breathingAudio.enabled = false;
    }
    private void Awake()
    {
        inputActionFairy.action.Enable();
        inputActionFairy.action.performed += goAway;
        InputSystem.onDeviceChange += onDeviceChange;
        canvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        inputActionFairy.action.Disable();
        inputActionFairy.action.performed -= goAway;
        InputSystem.onDeviceChange -= onDeviceChange;
    }
    private void Update()
    {
        if (pathControllerScript.reached)
        {
            textpanel.SetActive(true);
            questInfo();
        }

        if(dialgoueCount==4)
        {   
            timer += Time.deltaTime;
            breathingAudio.enabled = true;
            if (timer > 20f)
            {
                breathingAudio.enabled = false;
                actionCompleted = true;
            }
        }
        else if(dialgoueCount == 13)
        {
            timer += Time.deltaTime;
            if (timer > 8f)
            {
                DialogueCompleted = true;
                Debug.Log("DialogueCount");
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1, Time.deltaTime* speed);
            }
        }
        else
        {
            timer = 0f;
        }
    }

    void goAway(InputAction.CallbackContext context)
    {
        if(DialogueCompleted)
        {
            textpanel.SetActive(false);
            pathControllerScript.goAway = true;
        }
        else
        {
            textpanel.SetActive(true);
            next = true;
        }
    }

    void onDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch(change)
        {
            case InputDeviceChange.Disconnected:
                inputActionFairy.action.Disable();
                inputActionFairy.action.performed-=goAway;
                break;
            case InputDeviceChange.Reconnected:
                inputActionFairy.action.Enable();
                inputActionFairy.action.performed += goAway;
                break;

        }
    }


    void questInfo()
    {
        if (dialgoueCount == 0 || dialgoueCount == 1 || dialgoueCount == 2 || dialgoueCount == 3 || dialgoueCount == 7 || dialgoueCount == 9 || dialgoueCount == 11)
        {
            actionCompleted = true;
        }
        /*switch(dialgoueCount)
        {
            case 0:
                actionCompleted = true;
                if(!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if(actionCompleted)
                    {
                        updateDialogue(dialgoueCount + 1);
                    }
                    next = false;
                }
                break;
            case 1:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                    updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 2:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 3:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 4:
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 5:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 6:
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 7:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 8:
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 9:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 10:
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;
            case 11:
                actionCompleted = true;
                if (!next)
                {
                    updateDialogue(dialgoueCount);
                }
                else
                {
                    if (actionCompleted)
                        updateDialogue(dialgoueCount + 1);
                    next = false;
                }
                break;

        }*/
        if (dialgoueCount < dialogues.Length)
        {
            // Check if we should display the current dialogue
            if (!next)
            {
                updateDialogue(dialgoueCount); // Show current dialogue
            }
            else
            {
                if (actionCompleted)
                {
                    dialgoueCount++; // Move to the next dialogue
                    if (dialgoueCount < dialogues.Length)
                    {
                        updateDialogue(dialgoueCount, true); // Show next dialogue
                    }
                    next = false; // Reset 'next' for the next interaction
                }
                else
                {
                    next = !next;
                }
            }
        }
    }

    void updateDialogue(int i)
    {
        textpanel.GetComponentInChildren<TextMeshProUGUI>().text = dialogues[i];
        //actionCompleted = false;
        //next = false;
    }

    void updateDialogue(int i, bool dontChange)
    {
        textpanel.GetComponentInChildren<TextMeshProUGUI>().text = dialogues[i];
        actionCompleted = false;
        //next = false;
    }

}
