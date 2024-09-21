using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform player;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float slamSpeed;



   
[SerializeField] public float dashTime;
[SerializeField] public float dashSpeed;
public float time;



public float isDashing;
    
    [SerializeField] public GameObject boundaries;
    private bool isGrounded;
    private void Awake(){

        body = GetComponent<Rigidbody2D>();
        time = 0;
        
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

        if (Input.GetKey(KeyCode.LeftShift)){
            
        }
        //left and right movement multiplied by movement speed
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
        
        
        
        
        
        //press spacebar to jump
        if (Input.GetKey(KeyCode.Space) && isGrounded){
            Jump();
        }
        //trigger groundslam
        if (Input.GetKey(KeyCode.S) && (isGrounded == false)){
            GroundSlam();
        }
        
        
        if (Input.GetKey(KeyCode.LeftShift)){
           Dash(horizontalInput);
        }
        else if(!Input.GetKey(KeyCode.LeftShift)){
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
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

    private void GroundSlam(){
        body.velocity = new Vector2(0, slamSpeed);
        
    }
    
    private void Dash(float horizontalInput){
         body.velocity = new Vector2(horizontalInput * dashSpeed,body.velocity.y);
    } 
        
    
    

}
