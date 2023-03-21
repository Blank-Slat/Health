using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public float width;

    public float debrisTimer;
    public float debrisReducer;
    public float minDebrisTimer;

    public float heartTimer;
    public float heartReducer;
    public float minHeartTimer;

    private float currentDebrisTime;
    private float currentHeartTime;

    public GameObject debris;
    public GameObject heart;
    void FixedUpdate()
    {
        debrisTimer = timerChange(debrisTimer, debrisReducer, minDebrisTimer);
        heartTimer = timerChange(heartTimer, heartReducer, minHeartTimer);

        currentDebrisTime += Time.deltaTime;
        currentHeartTime += Time.deltaTime;

        if(currentDebrisTime >= debrisTimer)
        {
            currentDebrisTime = 0;
            spawn(debris);
        }
        if(currentHeartTime >= heartTimer)
        {
            currentHeartTime = 0;
            spawn(heart);
        }
    }

    float timerChange(float currentTimer, float reducer, float minTime)
    {
        currentTimer -= reducer * Time.deltaTime;
        currentTimer = Mathf.Clamp(currentTimer, minTime, 1000f);

        return currentTimer;
    }

    void spawn(GameObject prefab)
    {
        GameObject newObj = Instantiate(prefab);
        newObj.transform.position = new Vector2(Random.Range(-width, width), transform.position.y);
    }
}
