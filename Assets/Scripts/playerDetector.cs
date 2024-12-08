using UnityEngine;

public class playerDetector : MonoBehaviour
{
    public FairyUi fairyUiScript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("playerDetected");
            if (fairyUiScript.dialgoueCount == 5)
            {
                fairyUiScript.actionCompleted = true;
                Debug.Log("action Completed");

            }
        }
    }
}
