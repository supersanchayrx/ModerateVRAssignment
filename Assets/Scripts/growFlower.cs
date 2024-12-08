using UnityEngine;

public class growFlower : MonoBehaviour
{
    public bool grow=false;
    Vector3 growPos = new Vector3(-0.0540368669f, 0.319999993f, 0.00588122522f);
    public float speed;

    GameObject flower;
    private void Start()
    {
        flower = transform.Find("flower").gameObject;
    }
    private void Update()
    {
        if(grow && flower.transform.position!=growPos)
        {
            flower.transform.localPosition = Vector3.Lerp(flower.transform.localPosition, growPos,Time.deltaTime*speed);
        }

        if (Vector3.Distance(flower.transform.localPosition, growPos)<0.01f)
        {
            flower.transform.position=growPos;
        }
    }
}
