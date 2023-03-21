using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool damager;
    public AudioSource audioClip;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<playerController>() != null)
        {
            Debug.Log("Hit player");
            if (damager)
            {
                col.GetComponent<playerController>().health--;
            }
            else
            {
                col.GetComponent<playerController>().health++;
            }
            audioClip.Play();
            startDeath();
        }
        if(col.tag == "Kill")
        {
            startDeath();
        }
    }

    void startDeath()
    {
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Invoke("finalDeath", 3);
    }

    void finalDeath()
    {
        Destroy(gameObject);
    }
}
