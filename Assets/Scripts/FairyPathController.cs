using UnityEngine;
using Cinemachine;

public class FairyPathController : MonoBehaviour
{
    public CinemachineDollyCart dollyCart; // Reference to the Dolly Cart
    public CinemachineSmoothPath path; // Reference to the Cinemachine Path
    public Transform player; // Reference to the player
    public float returnSpeed = 2f; // Speed when returning to player
    public float flightDuration = 5f; // Duration for flight before returning

    public GameObject fairy;

    public float timer = 0f; // Timer for flight duration
    public bool isReturning = false; // Flag to determine if returning

    public bool goAway = false;

    public Vector3 dollyCartPos;

    public Animator anim;

    public bool reached = false;

    public AudioSource fairySounds;

    void Update()
    {
        dollyCartPos = dollyCart.transform.position;
        if (dollyCart == null || player == null || path == null) return;

        timer += Time.deltaTime;

        if (isReturning)
        {
            // Target position is the 0th waypoint in the path
           // Vector3 targetPosition = path.m_Waypoints[0].position;

            // Move towards the 0th waypoint
            /*dollyCart.transform.position = Vector3.Lerp(
                dollyCart.transform.position,
                targetPosition,
                returnSpeed * Time.deltaTime
            );*/
            dollyCart.m_Position = Mathf.MoveTowards(dollyCart.m_Position, 0f,Time.deltaTime*returnSpeed);

            // Check if the fairy is close enough to the 0th waypoint
            if (/*Vector3.Distance(dollyCart.transform.position, targetPosition) < 0.01f*/Mathf.Approximately(dollyCart.m_Position,0))
            {
                // Stop returning and reset dollyCart to start path movement
                dollyCart.m_Speed = 0f;
                anim.SetBool("idle", true);
                dollyCart.m_Position = 0f;
                reached = true; // Reset the position on the path
                fairySounds.mute = true;
                if (goAway)
                {
                    dollyCart.m_Speed = 1f;
                    isReturning = false;
                    reached = false;
                    //goAway =!goAway;
                    fairySounds.mute = false;
                    //timer = 0f;// Resume movement along the path
                }
                timer = 0f; // Reset the timer for the next flight
            }
        }
        else
        {
            anim.SetBool("idle", false);
            // Fairy is following the path
            if (timer >= flightDuration)
            {
                // Stop path movement and initiate return
                isReturning = true;
                //dollyCart.m_Speed = 0f; // Stop path movement
            }
            if (goAway)
            {
                if(dollyCart.m_Position==2.6f)
                {
                    goAway = false;
                    fairy.SetActive(false);
                    timer = 0f;
                }
            }
        }
    }
}
