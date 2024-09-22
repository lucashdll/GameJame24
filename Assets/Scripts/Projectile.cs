using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float speedReduction = 10.0f;

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
  
        this.targetTag = targetTag;
    }

    private void MoveProjectile()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {

        if (targetTag != null && targetTag.Contains(other.transform.tag))
        {
            if (targetTag.Contains("Player"))
            {
                other.GetComponent<Player>();
            }
        }
    }
}
