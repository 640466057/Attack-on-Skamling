using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    [Header("References")]
    public Rigidbody2D rb2d;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Vector3 projectilePlayerOffset;
    public UnityEngine.UI.Image healthBar;
    [SerializeField]
    private TMP_Text ballCounter;

    [Header("Parameteres")]
    public float moveSpeed;
    public float health;
    public float maxHealth;
    public int ammunition;
    
    [Header("Info")]
    [SerializeField]
    Vector3 mousePos;
    [SerializeField]
    float timeSinceDamageTaken;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        mousePos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        if (health > 0)
        {
            if (Input.GetMouseButtonDown(0) && ammunition > 0)
            {
                Vector3 projectilePos = new Vector3(transform.position.x + projectilePlayerOffset.x, transform.position.y + projectilePlayerOffset.y, transform.position.z + projectilePlayerOffset.z);
                Instantiate(projectile, projectilePos, transform.rotation);
                ammunition--;
            }

            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            rb2d.velocity = moveInput * moveSpeed;
        } else
        {
            SceneManager.LoadScene("Game Over");
        }
        timeSinceDamageTaken += Time.deltaTime;
        if (timeSinceDamageTaken > 5)
        {
            Heal(0.25f * Time.deltaTime);
        }

        ballCounter.text = ": " + ammunition;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        timeSinceDamageTaken = 0;
    }

    public void Heal(float healingAmount)
    {
        health += healingAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        
        healthBar.fillAmount = health / maxHealth;
    }
}