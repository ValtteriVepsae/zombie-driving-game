using System.Collections;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private bool grounded = false;
    [SerializeField] private float gravity;

    [SerializeField] private float health;
    bool hasCollided = false;
    float deadTime;
    [SerializeField] float despawnTime;
    public bool isDead;
    private float fallSpeed;
    private bool isResetting;

    [Header("Effects")]
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
                GetHit(_target, _target.gameObject, playerController.currentSpeed*player.GetComponent<Rigidbody>().mass);
            }
            if (playerController.currentSpeed > 5)
            {
                health -= Mathf.Abs(playerController.currentSpeed) * 6;
            }
        }
        if (_target.gameObject.tag.Equals("Projectile"))
        {
            
            projectileController = _target.gameObject.GetComponent<ProjectileController>();
            GetHit(_target, _target.gameObject, projectileController.knockback);
            projectileDamage = Random.Range(projectileController.damageMin, projectileController.damageMax);
            health -= projectileDamage;
            Debug.Log("target received : " + projectileDamage);
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
            if (!isDead)
            {
                GetComponent<MeshRenderer>().material = deadMaterial;
                isDead = true;
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

    void GetHit(Collision col, GameObject obj, float force)
    {
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        knockbackdirection = obj.transform.position - transform.position;
        rb.AddForce(knockbackdirection * -force, ForceMode.Impulse);
        hasCollided = true;
        contactPoint = col.GetContact(0);
        Instantiate(hitEffect, contactPoint.point, obj.transform.rotation);
        hitEffect.Play(true);
        effectTimer = 0;
        StartCoroutine("WakeUpTimer");
    }

    IEnumerator WakeUpTimer()
    {
        yield return new WaitForSeconds(5);
        isResetting = true;
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }
}
