using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] string[] enemiesType = { "Enemy1_standing", "Enemy2_Move", "Enemy3_Range" };
    [SerializeField] string type;
    float speed = 0.25f;
    [SerializeField] int health = 5;
    [SerializeField] int damage = 1;
    float endPosRight, endPosLeft;
    public float lineOfsite;
    public float shootingRange;
    int faceDir = 1;
    public float firerate = 1;
    private float nextfireTime;
    public GameObject bullet;
    private Transform player;
    PlayerAbility playerAbility;

    private Slider sliderLabel;
    public Slider sliderPrefab;
    public Transform sliderPos;

    Rigidbody2D Rigidbody2D;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        endPosRight = this.transform.position.x + 1;
        endPosLeft = this.transform.position.x - 1;

        sliderLabel = Instantiate(sliderPrefab, Vector3.zero, Quaternion.identity) as Slider;
        sliderLabel.transform.SetParent(canvas.transform);
        sliderLabel.maxValue = 5;
    }

    void Update()
    {
        Vector3 sliderLabelPos = Camera.main.WorldToScreenPoint(sliderPos.position);
        sliderLabel.transform.position = sliderLabelPos;
        sliderLabel.value = health;
        
        enemiesMove();

        if (health <= 0)
        {
            Debug.Log("Die");
            Destroy(this.gameObject);
            Destroy(sliderLabel.gameObject);
        }
    }
    void enemiesMove()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (type == enemiesType[0])
        {
            if (distanceFromPlayer < lineOfsite )
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
        }
        if (type == enemiesType[1])
        {
            if (distanceFromPlayer < lineOfsite)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                
                if (this.transform.position.x > endPosRight)
                {
                    
                    transform.eulerAngles = new Vector2(0, 0);
                    faceDir = -1;
                }
                else if (this.transform.position.x < endPosLeft)
                {
                    transform.eulerAngles = new Vector2(0, 180);
                    faceDir = 1;
                }
                Rigidbody2D.velocity = new Vector2(faceDir * speed, Rigidbody2D.velocity.y);
            }
        }
        if (type == enemiesType[2])
        {
            if (distanceFromPlayer < lineOfsite && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && nextfireTime < Time.time)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
                nextfireTime = Time.time + firerate;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfsite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            health -= 1;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(delayDamageToPlayer());
        }
    }
    IEnumerator delayDamageToPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        playerAbility.OntakeDamage(damage);
    }

}
