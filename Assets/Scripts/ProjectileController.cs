using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController: MonoBehaviour
{
    [Header("settings")]
    [SerializeField] float speed;
    [SerializeField] public float damageMin;
    [SerializeField] public float damageMax;
    [SerializeField] public float knockback;
    [SerializeField] float lifetime;
    private Rigidbody rb;
    private float time;
    [SerializeField] GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        knockback = 20;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        if (time >= lifetime)
        {
            Destroy(this.gameObject);
        }    
    }

    private void OnDestroy()
    {
        trail.transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
