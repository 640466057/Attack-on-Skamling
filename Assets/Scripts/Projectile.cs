using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{

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
    private Vector2 Debug;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempRot = mousePos - transform.position;
        rotation = Mathf.Atan2(tempRot.y, tempRot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        hight = 1;
        acceleration = 0.05f;
        
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            if (hight >= 0.5)
            {
                hight += acceleration * Time.deltaTime * 2;
                transform.localScale = new Vector3(hight, hight, hight);
                acceleration -= 1.5f * Time.deltaTime;
            }
            else
            {
                acceleration *= -0.5f;
                hight = 0.5f;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                rb.velocity /= 2.5f;
                if (acceleration < 0.075)
                {
                    rb.velocity = new Vector2(0, 0);
                    acceleration = 0;
                }
            }
        }
        Debug = rb.velocity;
    }
}