using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartFollow : MonoBehaviour
{
    public int healthIndex;
    public int listLength;
    private int prevLength;
    public Transform parent;
    public Vector3 offset;
    public Vector2[] posArray;
    void Start()
    {
        parent = transform.parent;
        listSet();
    }

    void FixedUpdate()
    {
        if(prevLength != listLength)
        {
            listSet();
        }
        else
        {
            transform.position = posArray[0];
            for (int i = 1; i < listLength; i++)
            {
                posArray[i - 1] = posArray[i];
            }
            posArray[listLength - 1] = parent.position + offset;
        }
    }

    void listSet()
    {
        prevLength = listLength;
        posArray = new Vector2[listLength];
        for (int i = 0; i < listLength; i++)
        {
            posArray[i] = parent.position + offset;
        }
    }
}
