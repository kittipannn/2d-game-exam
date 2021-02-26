using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    [SerializeField] string[] enemiesType = { "Enemy1_standing" , "Enemy2_Move", "Enemy3_Range" };
    [SerializeField] float speed;
    [SerializeField] int health = 5;
    [SerializeField] int damage = 1;

    public float lineOfsite;
    public float shootingRange;

    public float firerate = 1;
    private float nextfireTime;
    public GameObject bullet;
    private Transform player;
    PlayerAbility playerAbility;

    private Slider sliderLabel;
    public Slider sliderPrefab;
    public Transform sliderPos;

    bool detectPlayer = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");

        
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

        if (!detectPlayer)
        {

        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }
    void enemiesDetectPlayer() 
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfsite && distanceFromPlayer > shootingRange)
        {
            detectPlayer = true;
        }
        else if (distanceFromPlayer <= shootingRange && nextfireTime < Time.time )
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            nextfireTime = Time.time + firerate;
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
