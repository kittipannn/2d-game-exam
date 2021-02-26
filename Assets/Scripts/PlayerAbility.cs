using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    IsometricCharacterRenderer isometricCharacterRenderer;
    Vector2[] facingDir = { new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1), };
    public GameObject snowball;
    public int lastdir;
    //skill
    float cooldownShoot = 1.5f;
    bool useSkill = true;
    bool isCooldown = false;
    public Image CdButton;
    //health
    [SerializeField] int health = 5;
    private Slider sliderLabel;
    public Slider sliderPrefab;
    public Transform sliderPos;

    public GameObject panel;
    private void Awake()
    {
        isometricCharacterRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        CdButton.fillAmount = 0;
        Time.timeScale = 1;
        panel.SetActive(false);

    }
    private void Start()
    {
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
        checkHealthPlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastdir = isometricCharacterRenderer.lastDirection;
            spawnSnowball();
        }
        if (isCooldown)
        {
            CdButton.fillAmount -= 1 / cooldownShoot * Time.deltaTime;
        }
        if (CdButton.fillAmount <= 0)
        {
            CdButton.fillAmount = 0;
            isCooldown = false;
            useSkill = true;
        }
    }
    void spawnSnowball() 
    {
        Instantiate(snowball, new Vector3(this.transform.position.x+0.1f*facingDir[lastdir].x , this.transform.position.y + 0.1f * facingDir[lastdir].y, this.transform.position.z), Quaternion.identity);
    }
    public void onShootSnowball() 
    {
        if (useSkill)
        {
            lastdir = isometricCharacterRenderer.lastDirection;
            spawnSnowball();
            useSkill = false;
            CdButton.fillAmount = 1;
            StartCoroutine(cooldownSkill());
        }
    }
    IEnumerator cooldownSkill() 
    {
        yield return new WaitForSeconds(0.25f);
        isCooldown = true;
    }

    public void OntakeDamage(int damage)
    {
        health = health - damage;
    }
    void checkHealthPlayer() 
    {
        if (health <= 0)
        {
            health = 0;
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }
}
