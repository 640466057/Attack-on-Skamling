using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public PlayerController player;

    [Header("Parameteres")]
    public float speed;
    public bool playerBullet;
    private float rotation;
    private Rigidbody2D rb;

    [Header("Info")]
    [SerializeField]
    private float hight;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private bool hitSomthing = false;

    void Start()
    {
        //Debug.Log("Test");

        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempRot = mousePos - transform.position;
        rotation = Mathf.Atan2(tempRot.y, tempRot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        hight = 1f;
        acceleration = 0.05f;
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            if (hight >= 0.5)
            {
                hight += acceleration * Time.deltaTime * 2;
                transform.GetChild(0).transform.localScale = new Vector3(hight * 2, hight * 2, hight * 2);
                acceleration -= 1.5f * Time.deltaTime;
            }
            else
            {
                acceleration *= -0.5f;
                hight = 0.5f;
                transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                rb.velocity /= 2.5f;
                if (acceleration < 0.075)
                {
                    rb.velocity = new Vector2(0, 0);
                    acceleration = 0;
                }
            }
            transform.GetChild(0).transform.position = new Vector2(transform.position.x, transform.position.y + 2.25f * (hight - 0.5f) - 0.25f);
        }
        
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.24f, rb.velocity.normalized, rb.velocity.magnitude * Time.deltaTime);
        if (hit == true) {
            if (!hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("Ball"))
            {
                if (!hitSomthing)
                {
                    rb.velocity = Vector2.Reflect(rb.velocity, hit.normal);
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<ZombieAI>().health--;
                    }
                    hitSomthing = true;
                }
            }
        }
        else if (hitSomthing)
        {
            hitSomthing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Tag tag) && tag.tags.Contains(Tag.Tags.Player) && Mathf.Abs(rb.velocity.x) <= 0)
        {
            collision.gameObject.GetComponent<PlayerController>().ammunition++;
            Destroy(gameObject);
        }
    }
}