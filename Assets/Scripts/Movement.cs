using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform player;
    private BoxCollider2D boxCollider;
    [SerializeField] public GameObject boundaries;
//Layers
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
// basic movement stuff
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float groundDetectionDistance = 0.1f;
    [SerializeField] public float wallDetectionDistance = 420f;
    private float horizontalInput;
    private bool isGrounded;
    
// wall jump stuff
    public float wallJumpHeight = 10;
    public float wallJumpSpeed = 10;
    private float wallDirection;
    private bool onWall;
    private float walljumpTimer = 200000;
// groundslam stuf

// removed dash stuff
   

    
    
    
    
    
    
    
    
    private void Awake(){

        body = GetComponent<Rigidbody2D>();
        
        boxCollider = GetComponent<BoxCollider2D>();
        
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
        horizontalInput = Input.GetAxis("Horizontal"); //values -1, 0, 1 for determining direction player is facing

        //checks if player walljumped, sets velocity back to normal after
        walljumpTimer -= Time.deltaTime;
        if (walljumpTimer < 0){
        //left and right movement multiplied by movement speed
        body.AddForce(new Vector2(horizontalInput * moveSpeed, body.velocity.y));
        wallDirection = -horizontalInput;
        }
        
        
        
        
        //press spacebar to jump
        if (Input.GetKey(KeyCode.Space)){
            Jump();
        }
        
        //trigger groundslam
        // if (Input.GetKey(KeyCode.S) && (isGrounded == false)){
            // GroundSlam();
        // }
        
        
        

    
    }

    private bool IsGrounded(){//raycast check to see if on ground returns T/F
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        
    }
    private bool OnWall(){//raycast check to see if close to wall
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 420f, wallLayer);
        return raycastHit.collider != null;
    }


    private void Jump(){//called when space is pressed, checks if player close to ground or wall

        if (IsGrounded()){//regular jump
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        isGrounded = false;
        }
        else if (OnWall() && !IsGrounded()){//walljump
            // if(horizontalInput != 0){
            
            body.AddForce(new Vector2(Mathf.Sign(wallDirection * wallJumpSpeed) * 2000, 1000));
            transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            body.velocity = new Vector2(wallJumpSpeed * 2, wallJumpHeight*2);
            walljumpTimer = 2000000;
            // }
            
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "Ground"){
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Wall"){
            onWall = true;
            
        }
    }

    //private void GroundSlam(){
    //    body.velocity = new Vector2(0, slamSpeed);
        
   // }
    
    
        
    

}
