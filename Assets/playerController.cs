using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTurn;
    private float accelerateInput;
    private float steerInput;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentSteer;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        accelerateInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        if((accelerateInput != 0) && (currentSpeed <= maxSpeed))
        {
            currentSpeed += accelerateInput * 0.01f;
        }
        else if( currentSpeed < 0)
        {
            currentSpeed += 0.001f;
        }
        else if (currentSpeed > 0)
        {
            currentSpeed -= 0.001f;
        }
        currentSteer = steerInput * 100;
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        if(currentSpeed != 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * currentSteer);
        }
        if (currentSpeed > 0 && accelerateInput < 0)
        {
            currentSpeed -= 0.1f;
        }
    }
}
