using UnityEngine;
using System;

public class SurvivalTimer : MonoBehaviour
{
    public float goal;   // берётся из LevelData
    float t;
    bool won;

    public bool TimeCompleted => won;

    void Start()
    {
        goal = LevelController.instance.level.survivalTime;
    }

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
