using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChangeScript : MonoBehaviour
{
    public Color[] colorOptions;
    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        if (myLight == null) {
            Debug.LogError("Prefab doesn't have a Light component attached.");
        } else {
            SetRandomColor();
        }
    }

    void SetRandomColor() {
        if (colorOptions != null && colorOptions.Length > 0) {
            Color randomColor = colorOptions[Random.Range(0, colorOptions.Length)];
            myLight.color = randomColor;
        } else {
            Debug.LogError("No colors in the colorOptions array. Add colors in the Unity Editor.");
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
