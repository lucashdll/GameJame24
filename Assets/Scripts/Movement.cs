using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform player;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpHeight;
    

    [SerializeField] public GameObject boundaries;
    private bool isGrounded;
    private void Awake(){

        body = GetComponent<Rigidbody2D>();
    }



    // Start is called before the first frame update
    void Start()
    {
        //player 
    }

    // Update is called once per frame
    void Update()
    {
        //stores player facing direction for ez access
        float horizontalInput = Input.GetAxis("Horizontal"); 


        //left and right movement multiplied by movement speed
        body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);
        //press spacebar to jump
        if (Input.GetKey(KeyCode.Space) && isGrounded){
            Jump();
        }

        
    
    }

    private void Jump(){

        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "Ground"){
            isGrounded = true;
        }
    }
}
