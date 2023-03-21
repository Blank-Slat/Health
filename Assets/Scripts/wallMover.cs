using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMover : MonoBehaviour
{
    public Transform otherWall;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            otherWall.transform.position = transform.position + new Vector3(0, 22, 0);
        }
    }
}
