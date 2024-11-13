using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] GameObject tire_fl;
    [SerializeField] GameObject tire_fr;
    [SerializeField] GameObject tire_bl;
    [SerializeField] GameObject tire_br;
    [SerializeField] WheelCollider tire_fl_col;
    [SerializeField] WheelCollider tire_fr_col;
    [SerializeField] WheelCollider tire_bl_col;
    [SerializeField] WheelCollider tire_br_col;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSpawnPoint;
    private WeaponController weaponFire;
    public bool fireInput;


    public float accelerateInput;
    public float steerInput;
    int reverseGear;

    // Start is called before the first frame update
    void Start()
    {
        /*
        tire_fl_col = tire_fl.GetComponentInChildren<WheelCollider>();
        tire_fr_col = tire_fr.GetComponentInChildren<WheelCollider>();
        tire_bl_col = tire_bl.GetComponentInChildren<WheelCollider>();
        tire_br_col = tire_br.GetComponentInChildren<WheelCollider>();
        */
        tire_fl_col.brakeTorque = 0;
        tire_fr_col.brakeTorque = 0;
        tire_br_col.brakeTorque = 0;
        tire_bl_col.brakeTorque = 0;
        reverseGear = -1;

        if (weapon != null)
        {
            Instantiate(weapon, weaponSpawnPoint.transform.position, transform.rotation, transform);
            weaponFire = weapon.GetComponent<WeaponController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (accelerateInput > 0)
        {
            tire_fl_col.motorTorque = accelerateInput * Time.deltaTime * 100000 * -reverseGear;
            tire_fr_col.motorTorque = accelerateInput * Time.deltaTime * 100000 * -reverseGear;
            tire_fl_col.brakeTorque = 0;
            tire_fr_col.brakeTorque = 0;
        }
        if (accelerateInput < 0)
        {
            tire_fl_col.motorTorque = 0;
            tire_fr_col.motorTorque = 0;
            tire_fl_col.brakeTorque = -accelerateInput * Time.deltaTime * 10000;
            tire_fr_col.brakeTorque = -accelerateInput * Time.deltaTime * 10000;
        }

        if (steerInput != 0)
        {
            tire_fl_col.steerAngle = steerInput *10;
            tire_fr_col.steerAngle = steerInput * 10;

        }
        
        
       
    }
    void Update()
    {
        fireInput = Input.GetMouseButton(0);
        accelerateInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.R) && reverseGear == -1)
        {
            reverseGear = 1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            reverseGear = -1;
        }
    }
}
