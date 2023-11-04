using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce, gravityModifier;
    [SerializeField] private ParticleSystem explosionParticle, dirtParticle;
    [SerializeField] private AudioClip jumpSound, crashSound;

    private Animator playerAnimator;
    private AudioSource playerAudio;
    private Rigidbody playerRb;
    private bool isOnGround;
 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        isOnGround = true;
        Physics.gravity *= gravityModifier;
    }

    private void OnJump(InputValue input)
    {
        if (isOnGround && !GameManager.gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 2.0f);
           
        }
    }

    // Called when left mouse button is clicked
    private void OnFire(InputValue input)
    {
        // Perform a raycast from the camera to the click position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object is this object and have a specific tag
            if (hit.collider.gameObject == gameObject)
            {
                string otherTag = hit.collider.gameObject.tag;
                switch (otherTag)
                {
                    case "Player":
                        //GameManager.ChangeScore(-10);   //Subtracts 10 from the score
                        break;
                    case "Collectable":
                        //GameManager.ChangeScore(3);     //Adds 3 to the score
                        break;
                    case "Powerup":
                        jumpForce *= 2;                 //Gives player super jump power
                        Invoke("Powerup", 3);           //But only for 3 seconds
                        break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground" && !GameManager.gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Scoreable")
        {
            GameManager.ChangeScore(1);
        }

    }



    private void Powerup()
    {
        jumpForce *= 0.5f;
    }
}