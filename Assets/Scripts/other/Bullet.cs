using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 20;
    public Vector2 velocity;
    public bool isEnemy = false;
    public int hitDmg = 5;

    void Start()
    {
        //após sete segundos as balas irão desaparecer.
        Destroy(gameObject, 3);
    }


    void Update()
    {
        velocity = direction * speed;
        
    }

    private void FixedUpdate(){
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
