using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MovingPlat : MonoBehaviour
{
    public float speed = 0.06f, changeDirection = -1;
    Vector3 Move;
    // Start is called before the first frame update
    public PausedMenu pause;
    void Start()
    {
        Move = transform.position;
        pause = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PausedMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.pause)
        {
            this.transform.position = this.transform.position;
        }
        if(pause.pause == false)
        {
            Move.x += speed;
            transform.position = Move;
        }    
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            speed *= changeDirection;
        }
    }
}
