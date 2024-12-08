using UnityEngine;

public class startFire : MonoBehaviour
{
    public ParticleSystem fire;
    public bool rockshere;
    public fireFromRocks fireFromRocksScript;
    public Light fireLight;
    public FairyUi fairyUiScript;
    public bool setTrueOnce;
    public AudioSource fireAudio;

    void Start()
    {
        fire.Stop();
        rockshere = false;
        fireLight.enabled = false;
        setTrueOnce = false;
        fireAudio.enabled = false;
    }

    void Update()
    {
        if( rockshere && fireFromRocksScript.rockCollisionCount>=5)
        {
            fire.Play();
            fireLight.enabled = true;
            setTrueOnce = true;
            fireAudio.enabled = true;
        }

        if (setTrueOnce)
        {
            fairyUiScript.actionCompleted = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("fireRocks"))
        {
            rockshere=true;
        }
    }
}
