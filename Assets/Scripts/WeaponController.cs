using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject player;
    private CarController carController;
    [SerializeField] GameObject projectile;
    private GameObject shootPoint;
    [SerializeField] float interval;
    [SerializeField] float recoil;
    private float step;
    private GameObject aimPoint;
    [SerializeField] ParticleSystem shootEffect;
    Vector3 direction;
    [SerializeField] public GameObject hitbox;
    private ParticleSystem flameParticle;


    public void Start()
    {
        player = GameObject.Find("Celica");
        carController = player.GetComponent<CarController>();
        aimPoint = GameObject.Find("AimPoint");
        shootPoint = GameObject.Find("shootPoint");
        flameParticle = GetComponentInChildren<ParticleSystem>();
        if (hitbox)
        {
            hitbox.SetActive(false);
        }
    }

    public void FixedUpdate()
    {
        if (projectile)
        {
            if (step <= interval)
            {
                step++;
            }
            if (step >= interval && carController.fireInput == true)
            {
                Fire();
                step = 0;
            }
        }
        if(hitbox)
        {
            if(carController.fireInput)
            {
                hitbox.SetActive(true);
                flameParticle.Play();
            }
            else if (hitbox.activeSelf)
            {
                hitbox.SetActive(false);
                flameParticle.Stop();
            }
        }
        else if (carController.fireInput == false)
        {
            //hitbox.SetActive(false);
            shootEffect.Stop();
        }
        
        direction = (aimPoint.transform.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);

    }

    public void Fire()
    {
        if (projectile)
        {
            Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            Instantiate(shootEffect, shootPoint.transform.position, shootPoint.transform.rotation);
            transform.Rotate(Vector3.right, -recoil);
        }
    }
}

