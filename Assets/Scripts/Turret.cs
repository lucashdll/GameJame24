using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] private float speedReduction = 1.0f;
    [SerializeField] private float slowRate = 0.2f;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (GameManager.instance.player)
        { // aim at player
            transform.LookAt(GameManager.instance.player.transform.position); // look at the player
        }
    }
}
