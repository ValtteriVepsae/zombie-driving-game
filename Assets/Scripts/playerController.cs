using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTurn;
    [SerializeField] private float maxFallSpeed;

    [Header("Status")]
    [SerializeField] public float currentSpeed;
    [SerializeField] private float currentSteer;
    [SerializeField] private bool grounded;
    [SerializeField] private float gravity;
    GameObject weaponPoint;
    private float accelerateInput;
    private float steerInput;
    private float handBrakeInput;
    public bool fireInput;

    [Header("Prefabs")]
    [SerializeField] private GameObject weapon;
    private float fallSpeed;
    private WeaponController weaponFire;
    private enemyController enemyController;
    

    // Start is called before the first frame update
    void Start()
    {
        weaponPoint = GameObject.Find("WeaponPoint");
        grounded = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (weapon != null)
        {
            Instantiate(weapon, weaponPoint.transform.position, transform.rotation, transform);
            weaponFire = weapon.GetComponent<WeaponController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fireInput = Input.GetMouseButton(0);
        accelerateInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        if(fireInput)
        {
            //WeaponController.Fire();
        }

        //Check if accelerator is being pressed and if car is moving slower than maxSpeed
        if(accelerateInput != 0 && currentSpeed <= maxSpeed)
        {
            //Increase the car speed towards the pressed direction
            currentSpeed += accelerateInput * 0.1f;
        }
        //Check if accelerator is pressed and if car is moving backwards
        else if(accelerateInput == 0 && currentSpeed < 0)
        {
            //Decrease the car speed (adding to negative speed)
            currentSpeed += 0.01f;
        }
        //Check if accelerator is pressed and if car is moving forward
        else if (accelerateInput == 0 && currentSpeed > 0)
        {
            //Decrease car speed
            currentSpeed -= 0.01f;
        }
        //Check if accelerator is pressed and if car is slow enough to stop
        if (accelerateInput == 0 && currentSpeed < 0.01f && currentSpeed > 0.01f)
        {
            //Stop the car
            currentSpeed = 0;
        }
        //Move the car with the current speed value
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);

        //Make the current steering angle relative to the input and current speed
        //currentSteer = steerInput * currentSpeed * 10 - (currentSpeed * 0.9f);

        currentSteer = steerInput * 80;

        if (steerInput == 0)
        {
            currentSteer = 0;
        }
        //Check if car is moving
        if (currentSpeed > 0.1f || currentSpeed < -0.1f)
        {
            //Turn the car towards the steer direction
            transform.Rotate(Vector3.up * Time.deltaTime * currentSteer);
        }
        
        if (currentSpeed > 0 && accelerateInput < 0)
        {
            currentSpeed -= 0.1f;
        }

        //Check if car is on top of a collider
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
        {
            //Íf yes, car won't fall
            grounded = true;
            fallSpeed = 0;
        }
        else
        {
            //If no, car starts to fall with an increasing speed
            grounded = false;
            fallSpeed += gravity;
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && currentSpeed > 7)
        {
            currentSpeed -= currentSpeed / 10;
        }
        if(collision.gameObject.tag.Equals("Obstacle") && currentSpeed > 0)
        {
            Debug.Log("obstacle");
            currentSpeed = -1;
        }
    }
    
}
