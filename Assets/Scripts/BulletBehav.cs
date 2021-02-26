using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D rigidbody2D;
    CircleCollider2D collider2D;
    int damage = 1;

    private void Awake()
    {
        collider2D = GetComponent<CircleCollider2D>();
        collider2D.enabled = false;
    }
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rigidbody2D.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 3f);
        StartCoroutine(delayCollider());
    }
    IEnumerator delayCollider() 
    {
        yield return new WaitForSeconds(0.001f);
        collider2D.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerAbility>().OntakeDamage(damage);
            Destroy(this.gameObject);
        }
    }


}
