using Unity.VisualScripting;
using UnityEngine;

public class wateredPots : MonoBehaviour
{
    public FairyUi fairyUiScript;
    //public bool watered= false;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Colliding with smth");
        if(other.CompareTag("pots") && fairyUiScript.dialgoueCount ==6)
        {
            Debug.Log("colliding with pot");
            fairyUiScript.actionCompleted=true;
            other.GetComponent<growFlower>().grow = true;
        }
    }

}
