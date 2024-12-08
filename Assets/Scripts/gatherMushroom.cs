using UnityEngine;

public class gatherMushroom : MonoBehaviour
{
    public int mushroomCount=0;
    public bool setOnce;
    public FairyUi fairyScript;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("mushroom"))
        {
            mushroomCount++;
        }
    }

    private void Update()
    {
        if(mushroomCount>=2)
        {
            fairyScript.actionCompleted=true;
        }
    }
}
