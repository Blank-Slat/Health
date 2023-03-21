using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource deathClip;

    public GameObject[] deathSwitch;

    public highScoreDisplay hSD;
    public TextMeshProUGUI endScoreDisplay;
    public TextMeshProUGUI runtimeScoreDisplay;
    public int score;

    public Sprite upSprite;
    public Sprite downSprite;

    public int health;
    public float heartWeight;
    private int prevHealth;
    public List<heartFollow> hearts = new List<heartFollow>();
    public GameObject heartPrefab;
    public float minFlap;
    public float flapStrength;
    public float camOffset;
    public Transform cam;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Kill")
        {
            death();
        }
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        int newscore = Mathf.RoundToInt(transform.position.y / 2) * 10;
        if(score < newscore)
        {
            score = newscore;
        }
        runtimeScoreDisplay.text = score.ToString();

        if(rb.velocity.y > 0)
        {
            sr.sprite = upSprite;
        }
        else
        {
            sr.sprite = downSprite;
            movement();
        }

        if(prevHealth != health)
        {
            if(health == 0)
            {
                death();
            }
            else
            {
                heartSet();
            }
        }

        if (cam.transform.position.y + camOffset < transform.position.y)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y - camOffset, cam.transform.position.z);
        }
    }

    void movement()
    {
        float flap = flapStrength - (heartWeight * (health - 1));
        flap = Mathf.Clamp(flap, minFlap, 10000f);
        if (Input.GetKeyDown("a"))
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-0.5f, 1) * flap;
            jump.Play();
        }
        if (Input.GetKeyDown("d"))
        {
            sr.flipX = false;
            rb.velocity = new Vector2(0.5f, 1) * flap;
            jump.Play();
        }
    }

    void heartSet()
    {
        List<heartFollow> newList = new List<heartFollow>();
        prevHealth = health;
        for(int i = 0; i < health; i++)
        {
            if (hearts.Count > i)
            {
                newList.Add(hearts[i]);
            }
            else
            {
                if (i > 0)
                {
                    Debug.Log(i);
                    newList.Add(Instantiate(heartPrefab, newList[i - 1].transform).GetComponent<heartFollow>());
                }
                else
                {
                    newList.Add(Instantiate(heartPrefab, transform).GetComponent<heartFollow>());
                }
            }
        }
        hearts = newList;
        foreach (heartFollow hf in FindObjectsOfType<heartFollow>())
        {
            if (!hearts.Contains(hf))
            {
                Destroy(hf.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        cam.transform.position += transform.up * Time.deltaTime;
    }

    void death()
    {
        deathClip.Play();
        if(PlayerPrefs.GetInt("Highscore") < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        endScoreDisplay.text = "SCORE: " + score.ToString();
        hSD.showScore();
        foreach(GameObject g in deathSwitch)
        {
            g.SetActive(!g.activeInHierarchy);
        }
        rb.isKinematic = true;
        health = -1;
        rb.velocity = new Vector2(0, 0);
        GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<playerController>().enabled = false;
    }
}
