using UnityEngine;

public class followPlayerFairy : MonoBehaviour
{
    public Transform player; 
    public Vector3 referenceOffset = new Vector3(0, 2, 0); 
    public float radius = 1f; 
    public float speed = 2f; 
    public float verticalSpeed = 1f; 
    public float verticalAmplitude = 0.5f; 
    public float returnSpeed = 5f; 
    public float flightDuration = 5f; 

    public float angle = 0f; 
    public float timer = 0f; 
    public bool isReturning = false; 

    void Update()
    {
        if (player == null) return;

        if (!isReturning)
        {
            timer += Time.deltaTime;
            angle += speed * Time.deltaTime;

            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            float y = Mathf.Abs(Mathf.Sin(angle * verticalSpeed)) * verticalAmplitude;

            Vector3 offset = new Vector3(x, y, z);
            transform.position = player.position + offset;

            if (timer >= flightDuration)
            {
                isReturning = true;
                timer = 0f; 
            }
        }
        else
        {
            Vector3 targetPosition = player.position + referenceOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) <0.0001f)
            {
                isReturning = false; 
            }
        }

        transform.LookAt(player.position);
    }
}
