using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController: MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] public float damage;
    [SerializeField] float lifetime;
    private Rigidbody rb;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        if (time >= lifetime)
        {
            Destroy(this.gameObject);
        }    
    }
}
