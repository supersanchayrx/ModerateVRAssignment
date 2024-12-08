using UnityEngine;

public class waterCollision : MonoBehaviour
{
    FillBucket fillBucketScript;

    private void Start()
    {
        fillBucketScript = GameObject.Find("bucket").GetComponent<FillBucket>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name== "bucketWaterShader")
        {
            fillBucketScript.empty = false;
            Debug.Log("COllided");
        }
    }
}
