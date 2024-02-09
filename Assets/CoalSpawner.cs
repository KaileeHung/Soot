using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;

public class CoalSpawner : MonoBehaviour
{
    public GameObject coal;
    public GameObject candy;
    // seconds between spawns
    public float spawnRateCoal = 2;
    public float spawnRateCandy = 8;

    // number that will count up
    private float timerCoal = 0;

    // so coal and candy don't fall too closely
    public float offset = 10;

    public double deadZone = -5.27;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();   
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCoal < spawnRateCoal) {
            timerCoal += Time.deltaTime;
        } else {
            Spawn();
            timerCoal = 0;
        }

        CheckAndDestroyCoal();
    }

    void Spawn() {
        Camera mainCamera = Camera.main;
        float screenLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float coalX = UnityEngine.Random.Range(screenLeft, screenRight);

        CheckAndDestroyCoal();

        Instantiate(coal, new Vector3(coalX, transform.position.y), transform.rotation);

        float candyX = 0;
        int loopCounter = 0;
        int loopCap = 10;
        do {
            candyX = UnityEngine.Random.Range(screenLeft, screenRight);
            loopCounter++;
        } while (Math.Abs(candyX - coalX) < 5 && loopCounter < loopCap);
        if (loopCounter >= loopCap) {
            Debug.Log("infinite loop");
        }
        
        Instantiate(candy, new Vector3(candyX, transform.position.y), transform.rotation);
    }

    void CheckAndDestroyCoal() {
        GameObject[] coalInstances = GameObject.FindGameObjectsWithTag("Coal");

        foreach (GameObject coalInstance in coalInstances) {
            if (coalInstance.transform.position.y < deadZone) {
                Debug.Log("Coal deleted");
                Destroy(coalInstance);
            }
        }
    }
}
