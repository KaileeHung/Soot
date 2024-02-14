using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject fire;

    public float spawnRateFireMin;
    public float spawnRateFireMax;
    
    // number that will count up
    private float timerFire = 0;

    // off the left of screen
    public double deadZoneX = -9.7;
    public double startingY = -4.72;

    void Start()
    {
        Spawn();   
    }

    void Update()
    {  
        if (timerFire < UnityEngine.Random.Range(spawnRateFireMin, spawnRateFireMax)) {
            timerFire += Time.deltaTime;
        } else {
            Spawn();
            timerFire = 0;
        }

        CheckAndDestroyFire();
    }

    void Spawn() {
        CheckAndDestroyFire();
        Instantiate(fire, new Vector3(transform.position.x, (float) startingY), transform.rotation);
    }

    void CheckAndDestroyFire() {
        GameObject[] fireInstances = GameObject.FindGameObjectsWithTag("Fire");

        foreach (GameObject coalInstance in fireInstances) {
            if (coalInstance.transform.position.x < deadZoneX) {
                Destroy(coalInstance);
            }
        }
    }
}
