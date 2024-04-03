using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject player;
    private playerController playerController;
    [SerializeField] GameObject projectile;
    private GameObject shootPoint;
    [SerializeField] float interval;
    [SerializeField] float recoil;
    private float step;
    private GameObject aimPoint;
    [SerializeField] ParticleSystem shootEffect;
    Vector3 direction;


    public void Start()
    {
        player = GameObject.Find("Car");
        playerController = player.GetComponent<playerController>();
        aimPoint = GameObject.Find("AimPoint");
        shootPoint = GameObject.Find("shootPoint");
    }

    public void FixedUpdate()
    {
        if (step <= interval)
        {
            step++;
        }
        if (step >= interval && playerController.fireInput == true)
        {
            Fire();
            step = 0;
        }
        direction = (aimPoint.transform.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
    }
    public void Fire()
    {
            
            Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            Instantiate(shootEffect, shootPoint.transform.position, shootPoint.transform.rotation);
        transform.Rotate(Vector3.right, -recoil);
    }
}

