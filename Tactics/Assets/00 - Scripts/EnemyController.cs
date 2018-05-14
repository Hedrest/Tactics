﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;

    //shooting variables
    public float damage = 25f;
    public Vector3 shootPointOffset;
    private bool shooting;
    public float fireRate = 15f;
    public GameObject impactEffect;
    public float range = 50f;
    public float shotDelay = 0.2f;
    private float shotTimer;
    public float shootDistance = 10f;
    //public AudioClip shotClip;\

    // Enmemy Health system
    private Health currentHealth;
  


    public enum EnemyState { Patrol, Chase, Shoot, Aggro }
    public EnemyState enemyState;
    public float starWaitTime;
    private float waitTime;

    //patrol
    public Transform[] waypoint;
    private int randomSpot;
    public float investigateTimer;
    public float investigateWait=10f;


    void Start()
    {
        shotTimer = shotDelay;
      
        PickNewWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
       


        float moveSpeed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("MoveSpeed", moveSpeed);

        switch (enemyState)
        {
            case EnemyState.Patrol:    
                PatrolUpdate();
                break;
            case EnemyState.Chase:
                ChaseUpdate();
                break;
            case EnemyState.Shoot:
                ShootUpdate();
                break;
        }

    }
    bool InSightAngle(Vector3 point)
    {
        bool value = false;
        Vector3 lookDir = transform.forward;
        Vector3 pointDir = (point - transform.position).normalized;
        if (Vector3.Angle(lookDir, pointDir) < 160)
        {
            value = true;
        }

        return value;

    }
    bool ClearPathExists(Collider col)
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = (col.gameObject.transform.position - transform.position).normalized;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return (hit.collider == col);
    }

    void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position + shootPointOffset, transform.forward, out hit, range))
        {

            
            Health target = hit.transform.GetComponent<Health>();
            if (target != null)
            {
                
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);

        }
    }

        void ShootUpdate()
    {
        
      
        shotTimer -= Time.deltaTime;

        if (shotTimer <=0)
        {
            Shoot();
            shotTimer = shotDelay;
        }
        if (!SeekPlayer() || Vector3.Distance(transform.position, agent.destination) > shootDistance)
        {
            enemyState = EnemyState.Chase;
        }
    }



     void PickNewWaypoint()
    {
        randomSpot = Random.Range(0, waypoint.Length);
        waitTime = starWaitTime;
        agent.SetDestination(waypoint[randomSpot].transform.position);

    }
     public void PatrolUpdate()
    {
        
        if (SeekPlayer())
        {
            enemyState = EnemyState.Chase;
            agent.destination = transform.position;
        }
        
        if (Vector3.Distance(transform.position, waypoint[randomSpot].position) < 0.8f)
        {
            
            if (waitTime <= 0)
            {
                
                PickNewWaypoint();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void ChaseUpdate()
    {
        investigateTimer += Time.deltaTime;
        if (!SeekPlayer() && (investigateTimer >=investigateWait || Vector3.Distance(transform.position, agent.destination) < 1))
        {
            agent.SetDestination(transform.position);
            transform.LookAt(transform.position);

            if (investigateTimer >= investigateWait)
            {
                enemyState = EnemyState.Patrol;

                PickNewWaypoint();
                investigateTimer = 0f;
            }
     
        }
        if (Vector3.Distance(transform.position, target.position) <= shootDistance)
        {
            enemyState = EnemyState.Shoot;
        }
    }


    bool SeekPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 10);
        foreach (Collider hit in hits)
        {
            if (hit.tag == "Player" && InSightAngle(hit.gameObject.transform.position) && ClearPathExists(hit))
            {
                agent.destination = hit.transform.position;
               
                return true;
            }

        }
        return false;
    }


}




