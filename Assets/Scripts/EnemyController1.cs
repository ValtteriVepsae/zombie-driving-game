using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController1 : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public int knockedOutTime;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GameObject player;

    NavMeshAgent navAgent;
    Transform navTarget;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        navAgent = GetComponent<NavMeshAgent>();
        navTarget = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.isActiveAndEnabled)
        {
            navAgent.SetDestination(navTarget.localPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {            
            ProjectileController projectile = collision.gameObject.GetComponent<ProjectileController>();
            float damage = Random.Range(projectile.damageMin, projectile.damageMax);
            currentHealth =- damage;
            ContactPoint contactPoint = collision.GetContact(0);
            ParticleSystem vfx = Instantiate(hitEffect, contactPoint.point, collision.gameObject.transform.rotation);
            vfx.transform.parent = null;
            
            Debug.Log(gameObject.name + " hit by " + collision.gameObject.name + " for " + damage + " damage");
            navAgent.enabled = false;
            StartCoroutine("WakeUpTimer");
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.rigidbody.velocity.x > 1 && collision.rigidbody.velocity.z > 1)
            {
                navAgent.enabled = false;
                StartCoroutine("WakeUpTimer");
            }
        }
    }

    public IEnumerator WakeUpTimer()
    {
        yield return new WaitForSeconds(knockedOutTime);
        navAgent.enabled = true;
    }
}
