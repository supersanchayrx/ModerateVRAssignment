using UnityEngine;

public class FillBucket : MonoBehaviour
{
    public bool empty = true; // true if the glass is empty
    public bool pour = false; // true if the glass should pour water
    public GameObject waterMesh; //  mock mesh
    public ParticleSystem waterPour; // particlesuystm

    public float tiltThreshold = 100f; // Threshold of angle
    private bool isPouring = false; // flags to allow

    private ParticleSystem.EmissionModule waterEmission; // this was not accesible from unity to modify directly so created a copied struct, modified it, then sent the values to the unity protected system!

    MeshRenderer waterMeshRenderer;
    //public float timer;

    float tempTimer;

    bool resetted;

    private void Start()
    {
        empty = true;
        waterMeshRenderer = waterMesh.GetComponent<MeshRenderer>();
        waterPour.Stop();
        //timer = 0f;
        waterMeshRenderer.enabled = false;
        // emission ref
        waterEmission = waterPour.emission;
        resetted = false;
    }

    private void Update()
    {
        if (!empty)
        {
            //waterMesh.SetActive(true);
            waterMeshRenderer.enabled = true;


            float tiltAngle = Vector3.Angle(Vector3.up, transform.up);

            if (tiltAngle > tiltThreshold && !empty)
            {
                if (!isPouring)
                {
                    StartPouring();
                }
                else
                {
                    SetEmissionEnabled(true);
                }
            }
            else
            {
                if (isPouring)
                {
                    //tempTimer = timer;
                    StopEmission();
                }
            }

            if (/*isPouring && !waterPour.isPlaying && tiltAngle <= tiltThreshold*/waterPour.particleCount==1000 && !resetted)
            {//this is cycle check
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
            //not needed anymore
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
        //setting bools

        waterPour.Play();
        SetEmissionEnabled(true);
        isPouring = true;
        Debug.Log("Pouring started.");
        //timer += Time.deltaTime;
    }

    void StopEmission()
    {
        //timer = tempTimer;
        SetEmissionEnabled(false);
        Debug.Log("Pouring stopped as the tilt angle is below the threshold.");
        
    }

    void SetEmissionEnabled(bool enabled)
    {
        var emission = waterPour.emission; 
        emission.enabled = enabled;       
        //timer += Time.deltaTime;
    }

    void EmptyGlass()
    {
        //waterMesh.SetActive(false);
        waterMeshRenderer.enabled = false;
        empty = true;
        pour = false; // Reset pour state
        Debug.Log("The glass is now empty!");
        resetted = !resetted;
        waterPour.Clear();

    }
}
