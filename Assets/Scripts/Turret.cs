using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
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
            // shoot at player if they're within range
            if (Vector2.Distance(this.transform.position, GameManager.instance.player.transform.position) <= 12)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (Time.time > fireTime)
        {
            var bullet = Instantiate(projectile, transform.position, transform.rotation);
            bullet.GetComponent<Projectile>().Init(speed, speedReduction);
            fireTime = Time.time + fireRate;
        }
    }
}
