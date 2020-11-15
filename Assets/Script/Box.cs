using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int Health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Damage(int damage)
    {
        Health -= damage;
    }
    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
