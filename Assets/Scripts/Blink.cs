using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {
    public float min = 2;
    public float max = 25;
    public float circleTime = 2;
    private Light light;
    int dir = 1;
    float range;
    private void Start()
    {
        light = GetComponent<Light>();
        range = max - min;
    }

    private void Update()
    {
        if (light.intensity > max)
            dir = -1;
        else if (light.intensity < min)
            dir = 1;
        light.intensity += dir * range * Time.deltaTime / circleTime;
    }
}
