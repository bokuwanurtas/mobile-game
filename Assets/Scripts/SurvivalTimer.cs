using UnityEngine;
using System;

public class SurvivalTimer : MonoBehaviour
{
    public float goal = 60f;
    float t;
    bool won;

    public bool TimeCompleted => won;

    void Update()
    {
        t += Time.deltaTime;
        if (t < goal){
            Debug.Log(Math.Round(t));
        }
        

        if (!won && t >= goal)
        {
            won = true;
            Debug.Log("Survival time completed!");
        }
    }
}
