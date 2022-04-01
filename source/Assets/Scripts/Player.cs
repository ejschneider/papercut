using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] int maxJumps = 3;
    [SerializeField] Vector2 deathKick= new Vector2(100f,100f);
    [SerializeField] AudioClip deathSound;

    Rigidbody2D playerRigidbody;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    Animator playerAnimator;
    

    
    int currentJumps = 1;
    private bool jumping = false;
    float timeStartedJumping;
    bool isAlive = true;
    

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {if (!isAlive) { return; }
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { playerAnimator.SetBool("Jumping", false); }
        Run();
        Jump();
        FlipSprite();
        Die();

        
    }

   

    private void Run()
    {
        
        float controlThrow= CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 horizontalVelocity = new Vector2(controlThrow * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = horizontalVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void Jump()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            currentJumps = 1;

        }
        if (maxJumps <= currentJumps)
        {

            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && maxJumps >= currentJumps)
        {
            playerAnimator.SetBool("Jumping", true);




            Vector2 verticalVelocity = new Vector2(0f, jumpSpeed);
            playerRigidbody.velocity = verticalVelocity;
            currentJumps++;



        }
    }
        private void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().PlayerDeath();
        }



    }
    /*
    private void Float()
    {

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Debug.Log("Floating");
            playerRigidbody.gravityScale = 0.0f;
        }
            
        
        
    }
    */
}

