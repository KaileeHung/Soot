using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingSpawner : MonoBehaviour
{
    public GameObject coal;
    public GameObject candy;
    // seconds between spawns
    public float spawnRateCoalMin;
    public float spawnRateCoalMax;
    public float spawnRateCandyMin;
    public float spawnRateCandyMax;

    // number that will count up
    private float timerCoal1 = 0;
    private float timerCoal2 = 2;
    private float timerCandy1 = 0;
    private float timerCandy2 = 2;
    private Camera mainCamera;
    private float screenLeft;
    private float screenMiddle;
    private float screenRight;

    public double deadZoneY = -5.27;
    private string[] obstacleTagList = {"Coal", "Candy"};
    // private float tempX = 0;
    private Queue<float> posXQueue = new Queue<float>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        screenLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenMiddle = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, 0)).x;
        screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        Debug.Log("left: " + screenLeft);
        Debug.Log("right: " + screenRight);

        posXQueue.Enqueue(Spawn(coal, UnityEngine.Random.Range(-8, 8)));
    }

    // Update is called once per frame
    void Update()
    {


        if (timerCoal1 < UnityEngine.Random.Range(spawnRateCoalMin, spawnRateCoalMax+1)) {
            timerCoal1 += Time.deltaTime;
        } else {
            Spawn(coal, AdjustQueue());
            timerCoal1 = 0;
        }

        if (timerCoal2 < UnityEngine.Random.Range(spawnRateCoalMin, spawnRateCoalMax+1)) {
            timerCoal2 += Time.deltaTime;
        } else {
            Spawn(coal, AdjustQueue());
            timerCoal2 = 1;
        }

        if (timerCandy1 < UnityEngine.Random.Range(spawnRateCandyMin, spawnRateCandyMax+1)) {
            timerCandy1 += Time.deltaTime;
        } else {
            Spawn(candy, AdjustQueue());
            timerCandy1 = 0;
        }

        if (timerCandy2 < UnityEngine.Random.Range(spawnRateCandyMin, spawnRateCandyMax+1)) {
            timerCandy2 += Time.deltaTime;
        } else {
            Spawn(candy, AdjustQueue());
            timerCandy2 = 2;
        }

        CheckandDestroyAllObstacles(obstacleTagList);
    }

    // pick a position +- a certain offset of the previous. randomly pick +-

    private float AdjustQueue() {
        int xPos;
        bool goodVal;
        int loopCounter = 0;
        int loopCap = 50;
        do {
            xPos = UnityEngine.Random.Range(-8, 8);
            goodVal = true;
            foreach (float position in posXQueue) {
                if (xPos == position) {
                    goodVal = false;
                    break;
                }
            }
            loopCounter++;
        } while (!goodVal && loopCounter < loopCap);
        if (loopCounter >= loopCap) {
            Debug.Log("infinite loop");
        }

        posXQueue.Enqueue(xPos);
        if (posXQueue.Count >= 6) {
            posXQueue.Dequeue();
        }
        Debug.Log("xpos: " + xPos);
        return xPos;
    }

    float Spawn(GameObject gameObject, float xpos) {
        Instantiate(gameObject, new Vector3(xpos, transform.position.y), transform.rotation);
        return xpos;
    }

    void CheckandDestroyAllObstacles(string[] tagList) {
        foreach (string tag in tagList) {
            CheckAndDestroyObstacle(tag);
        }
    }
    private void CheckAndDestroyObstacle(String gameObstacleTag) {
        GameObject[] instances = GameObject.FindGameObjectsWithTag(gameObstacleTag);
        foreach (GameObject instance in instances) {
            if (instance.transform.position.y < deadZoneY) {
                Destroy(instance);
                Debug.Log("infinite loop");
            }
        }
    }
}
