using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    [Header("References")]
    public Rigidbody2D rb2d;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Vector3 projectilePlayerOffset;

    [Header("Parameteres")]
    public float moveSpeed;
    [SerializeField]
    public float health;
    [SerializeField]
    public int ammunition;
    
    [Header("Info")]
    [SerializeField]
    Vector3 mousePos;

    void Start()
    {
        
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
            //Debug.Log("_Game Over_");
        }
    }
}