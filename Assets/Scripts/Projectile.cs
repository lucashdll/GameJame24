using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float speedReduction = 1.0f;

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        MoveProjectile();
    }

    public void Init(float speed, float speedReduction)
    {
        this.speed = speed;
        this.speedReduction = speedReduction;
  
    }

    private void MoveProjectile()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag != "Turret")
        {
            if (other.transform.tag == "Player" && other.transform.GetComponent<_Movement>().speed > 1)
            {
                // reduce player speed upon projectile hit
                other.transform.GetComponent<_Movement>().speed -= speedReduction;
                //Debug.Log(other.transform.GetComponent<Movement>().moveSpeed);
            }
            Destroy(this.gameObject);
        }
    }
}
