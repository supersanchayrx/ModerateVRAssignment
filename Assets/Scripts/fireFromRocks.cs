using UnityEngine;

public class fireFromRocks : MonoBehaviour
{
    public startFire startFireScript;
    public int rockCollisionCount = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("fireRocks"))
        {
            if ((startFireScript.rockshere) && rockCollisionCount < 5)
            {
                rockCollisionCount++;
            }
        }
        
    }
}
