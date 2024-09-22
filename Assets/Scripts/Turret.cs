using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Turret turret;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float fireRate = 3.0f;

    [SerializeField]
    private float fireTime = 3f;

    [SerializeField]
    public float speedReduction = 1.0f;

    [SerializeField]
    private float speed = 100.0f;

    void Update()
    {
        if (GameManager.instance.player != null)
        {
            transform.LookAt(GameManager.instance.player.transform);
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time > fireTime)
        {
            var bullet = Instantiate(projectile, transform.position, transform.rotation);
            bullet.GetComponent<Projectile>().Init(speed, speed);
            fireTime = Time.time + fireRate;
        }
    }
}
