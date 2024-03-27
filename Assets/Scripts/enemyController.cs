using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyController : MonoBehaviour
{

    [SerializeField] private bool grounded = false;
    [SerializeField] private float gravity;
    private float fallSpeed;
    [SerializeField] private float health;
    bool hasCollided = false;
    float deadTime;
    [SerializeField] float despawnTime;

    [SerializeField] Material aliveMaterial;
    [SerializeField] Material deadMaterial;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem deathEffect;
    private GameObject player;
    private playerController playerController;
    ProjectileController projectileController;
    static float projectileDamage;
    private float effectTimer = -1;
    ContactPoint contactPoint;
    Vector3 knockbackdirection;
    private bool isResetting;

    void Start()
    {
        player = GameObject.Find("Car");
        playerController = player.GetComponent<playerController>();
        deadTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided == false)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
            {
                //If yes, enemy won't fall
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

    }
    void OnCollisionEnter(Collision _target)
    {
        if (_target.gameObject.tag.Equals("Player"))
        {
            if (_target.gameObject != null)
            {
                getHit(_target, _target.gameObject);
            }
            if (playerController.currentSpeed > 5)
            {
                health -= Mathf.Abs(playerController.currentSpeed) * 6;
            }
        }
        if (_target.gameObject.tag.Equals("Projectile"))
        {
            getHit(_target, _target.gameObject);
            projectileController = _target.gameObject.GetComponent<ProjectileController>();
            projectileDamage = projectileController.damage;
            health -= projectileDamage;
            Destroy(_target.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (effectTimer >= 0 && effectTimer < 10)
        {
            effectTimer++;
        }
        else if (effectTimer >= 10)
        {
            hitEffect.Stop(true);
        }
        if (health <= 0)
        {
            if (GetComponent<MeshRenderer>().material != deadMaterial)
            {
                GetComponent<MeshRenderer>().material = deadMaterial;
            }
            deadTime++;
        }
        if (deadTime >= despawnTime)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (isResetting && transform.rotation != Quaternion.identity && health > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);           
        }
        else if (isResetting)
        {
            isResetting = false;
        }
    }

    void getHit(Collision col, GameObject obj)
    {
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        knockbackdirection = obj.transform.position - transform.position;
        rb.AddForce(knockbackdirection * -100, ForceMode.Impulse);
        hasCollided = true;
        contactPoint = col.GetContact(0);
        Instantiate(hitEffect, contactPoint.point, obj.transform.rotation);
        hitEffect.Play(true);
        effectTimer = 0;
        StartCoroutine("wakeUpTimer");
    }

    IEnumerator wakeUpTimer()
    {
        yield return new WaitForSeconds(5);
        isResetting = true;
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }
}
