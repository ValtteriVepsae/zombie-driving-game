using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    ParticleSystem particle;
    [SerializeField] float lifetime;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
        particle.Play(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        if (time > lifetime) 
        {
            particle.Stop(true);
        }
    }
}
