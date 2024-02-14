using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class weaponFire : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private Transform pos;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void Fire()
    {
        pos = player.transform;
        Debug.Log(transform.rotation);
        Instantiate(projectile, pos.position, transform.rotation);
    }
}
