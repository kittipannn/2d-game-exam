using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowballBehav : MonoBehaviour
{
    Rigidbody2D rb;
    private float speedSlash = 7.5f;
    float x, y;
    PlayerAbility ability;

    Vector2[] facingDir = { new Vector2(0,1), new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1), };
    void Start()
    {
        ability = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
        rb = GetComponent<Rigidbody2D>();
        x = facingDir[ability.lastdir].x;
        y = facingDir[ability.lastdir].y;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x * speedSlash,  y * speedSlash);
        Destroy(this.gameObject, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
