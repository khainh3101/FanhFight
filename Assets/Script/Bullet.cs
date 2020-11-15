using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger == false)
        {
            if (collision.CompareTag("Player"))
            {
                collision.SendMessageUpwards("Damage", 1);
            }
            Destroy(gameObject);
        }
        if (collision.CompareTag("actacktrigger"))
        {
            Destroy(gameObject);

        }
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }

    }
}
