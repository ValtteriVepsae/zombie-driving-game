using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    [SerializeField] private bool grounded = false;
    [SerializeField] private float gravity;
    private float fallSpeed;
    [SerializeField] private float health = 5;
    bool hasCollided = false;
    [SerializeField] Material aliveMaterial;
    [SerializeField] Material deadMaterial;
    [SerializeField] private Rigidbody rb;
    private GameObject player;
    public playerController cs;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Car");
        cs = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided == false)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
            {
                //Íf yes, enemy won't fall
                grounded = true;
                fallSpeed = 0;
            }
            else
            {
                //If no, enemy starts to fall with an increasing speed
                grounded = false;
                fallSpeed += gravity;
                transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
            }
        }
        Debug.Log(health);
        if (health <= 0)
        {
            GetComponent<MeshRenderer>().material = deadMaterial;
        }
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Player"))
        {
            rb.freezeRotation = false;
            rb.useGravity = true;
            hasCollided = true;
            if(cs.currentSpeed > 5)
            {
                health -= Mathf.Abs(cs.currentSpeed);
            }

        }
    }
}