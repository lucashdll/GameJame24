using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class _Movement : MonoBehaviour
{
    // basic movement
private float horizontalInput;
[SerializeField]private float speed = 8f;
[SerializeField]private float jumpHeight = 16f;
private bool isFacingRight = true;
// wallsliding 
private bool isWallsliding;
private bool isWallJumping;
private float wallJumpingDirection;
private float wallJumpingTime = 2f;
private float wallJumpingCounter;
private float wallJumpingDuration = 0.4f;
private Vector2 wallJumpingPower = new Vector2(8f, 16f);



[SerializeField]private float wallSlidingSpeed = 2f;

[SerializeField] private Rigidbody2D body;

[SerializeField] private Transform groundCheck;

[SerializeField] private LayerMask groundLayer;

[SerializeField] private Transform wallCheck;

[SerializeField] private LayerMask wallLayer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }
        if (Input.GetKeyDown(KeyCode.Space) && (body.velocity.y > 0f)){
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }
        WallJump();
        WallSlide();
        if(!isWallJumping){
        Flip();
        }

       


    }
    // Start of Methods
    private void FixedUpdate(){//from copied code, not sure why this is here
        if(!isWallJumping){
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private bool IsWalled(){
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide(){
        if (IsWalled() && !IsGrounded() && horizontalInput != 0f){
            isWallsliding = true;
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else{
            isWallsliding = false;
        }
    }
        private void WallJump(){
            if (isWallsliding){
                isWallJumping = false;
                wallJumpingDirection = -transform.localScale.x;
                wallJumpingCounter = wallJumpingTime;

                CancelInvoke(nameof(StopWallJumping));
            }
            else{
                wallJumpingCounter -= Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f){
                isWallJumping = true;
                body.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;

                if (transform.localScale.x != wallJumpingDirection){
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
    
    private void StopWallJumping(){
        isWallJumping = false;
    }


    private void Flip(){
        if (isFacingRight && horizontalInput <0f || !isFacingRight && horizontalInput>0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }


    }





}
