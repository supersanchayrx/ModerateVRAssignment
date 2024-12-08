using UnityEngine;

public class FillBucket : MonoBehaviour
{
    public bool empty = true; // True if the glass is empty
    public bool pour = false; // True if the glass should pour water
    public GameObject waterMesh; // Visual representation of water in the glass
    public ParticleSystem waterPour; // Particle system for pouring water

    public float tiltThreshold = 100f; // Tilt angle threshold for pouring
    private bool isPouring = false; // Ensures the pouring logic runs only once

    private ParticleSystem.EmissionModule waterEmission; // Emission module reference

    MeshRenderer waterMeshRenderer;
    //public float timer;

    float tempTimer;

    bool resetted;

    private void Start()
    {
        // Initialize the state of the glass and water
        empty = true;
        waterMeshRenderer = waterMesh.GetComponent<MeshRenderer>();
        waterPour.Stop();
        //timer = 0f;
        waterMeshRenderer.enabled = false;
        // Cache the EmissionModule reference
        waterEmission = waterPour.emission;
        resetted = false;
    }

    private void Update()
    {
        if (!empty)
        {
            //waterMesh.SetActive(true);
            waterMeshRenderer.enabled = true;


            // Check the tilt angle of the glass
            float tiltAngle = Vector3.Angle(Vector3.up, transform.up);

            // If the tilt is above the threshold and the glass is not empty
            if (tiltAngle > tiltThreshold && !empty)
            {
                if (!isPouring)
                {
                    StartPouring();
                }
                else
                {
                    // Ensure emission is enabled while pouring
                    SetEmissionEnabled(true);
                }
            }
            else
            {
                // Stop emitting new particles if the tilt is below the threshold
                if (isPouring)
                {
                    //tempTimer = timer;
                    StopEmission();
                }
            }

            // Check if the particle system has completed its cycle
            if (/*isPouring && !waterPour.isPlaying && tiltAngle <= tiltThreshold*/waterPour.particleCount==1000 && !resetted)
            {
                isPouring = false; // Reset pouring state
                EmptyGlass(); // Empty the glass
                Debug.Log("bucket is now empty");
            }

            /*if(timer>10f)
            {
                isPouring = false; // Reset pouring state
                EmptyGlass(); // Empty the glass
                Debug.Log("bucket is now empty");
            }*/
        }

        else
        {
            //waterMesh.SetActive(false );
            waterMeshRenderer.enabled = false;
            waterPour.Stop();
        }
    }

    void StartPouring()
    {
        // Play the particle system and set the pouring flag
        waterPour.Play();
        SetEmissionEnabled(true);
        isPouring = true;
        Debug.Log("Pouring started.");
        //timer += Time.deltaTime;
    }

    void StopEmission()
    {
        //timer = tempTimer;
        // Disable new particle emission but keep the existing particles alive
        SetEmissionEnabled(false);
        Debug.Log("Pouring stopped as the tilt angle is below the threshold.");
        
    }

    void SetEmissionEnabled(bool enabled)
    {
        // Safely modify the EmissionModule's enabled property
        var emission = waterPour.emission; // Get a copy of the struct
        emission.enabled = enabled;       // Modify the copy
        //timer += Time.deltaTime;
    }

    void EmptyGlass()
    {
        // Deactivate water mesh and mark the glass as empty
        //waterMesh.SetActive(false);
        waterMeshRenderer.enabled = false;
        empty = true;
        pour = false; // Reset pour state
        Debug.Log("The glass is now empty!");
        resetted = !resetted;
        waterPour.Clear();

    }
}
