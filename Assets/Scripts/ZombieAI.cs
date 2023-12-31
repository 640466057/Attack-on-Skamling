using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform target;

    [Header("Parameteres")]
    [SerializeField]
    public float health;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float followDistance;
    //public float speed;

    [Header("Info")]
    [SerializeField]
    private float attackCooldown;
    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if (followDistance >= Vector3.Distance(target.position, transform.position))
            {
                agent.SetDestination(target.position);
            }
        } else
        {
            agent.SetDestination(transform.position);
            Destroy(gameObject, 2.5f);
        }
        
        attackCooldown -= Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Tag tag) && tag.tags.Contains(Tag.Tags.Player) && attackCooldown <= 0 && health > 0)
        {
            if (attackCooldown <= 0)
            {
                collision.gameObject.GetComponent<PlayerController>().health -= attackDamage;
                attackCooldown = 60 / attackSpeed;
            }
        }
    }
}
