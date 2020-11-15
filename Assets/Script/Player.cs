using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 4, maxjump = 4, jumpPow = 220f;
    public bool grounded = true, facerright = true, doublejump = false;
    public int ourHealth;
    public int maxhealth = 4;
    public Rigidbody2D r2;
    public Animator anim;
    public GameMaster gm;
    public SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();   
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxhealth;
        sound = GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundManager>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                grounded = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow);
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * jumpPow);
                }
            }
                
        }
    }
    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);
        
        //giới hạn tốc độ
        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);


        //giới hạn độ cao nhảy
        if (r2.velocity.y > maxjump)
            r2.velocity = new Vector2(r2.velocity.x, maxjump);
        if(r2.velocity.y < -maxjump)
            r2.velocity = new Vector2(r2.velocity.x, -maxjump);

        if (h >0 && !facerright)
        {
            Flip();
        }
        if (h < 0 && facerright)
        {
            Flip();
        }
        if(grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }
        if (ourHealth <= 0)
        {
            Death();
        }

    }
    public void Flip()
    {
        facerright = !facerright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(PlayerPrefs.GetInt("highscore") < gm.points)
        {
            PlayerPrefs.SetInt("highscore", gm.points);
        }
       
    }
    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("RedFlash");
    }
    public void KnockBack (float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x - 100, Knockdir.y * Knockpow));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            sound.Playsound("coins");
            Destroy(collision.gameObject);
            gm.points += 1;

        }
        if (collision.CompareTag("heart"))
        {
            Destroy(collision.gameObject);
            ourHealth = 5;
        }
        if (collision.CompareTag("shoes"))
        {
            Destroy(collision.gameObject);
            maxspeed = 6;
            speed = 100f;
            StartCoroutine(timecount(5));
        }

    }
    IEnumerator timecount (float time)
    {
        yield return new WaitForSeconds(time);
        maxspeed = 3;
        speed = 50f;
        yield return 0;
            
    }

}
