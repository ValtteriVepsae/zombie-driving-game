using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponFire : MonoBehaviour
{
    private GameObject player;
    private playerController playerController;
    [SerializeField] GameObject projectile;
    private GameObject shootPoint;
    [SerializeField] float interval;
    private float step;
    private GameObject aimPoint;
    Vector3 direction;


    public void Start()
    {
        player = GameObject.Find("Car");
        playerController = player.GetComponent<playerController>();
        aimPoint = GameObject.Find("AimPoint");
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
            shootPoint = GameObject.Find("shootPoint");
            Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }

