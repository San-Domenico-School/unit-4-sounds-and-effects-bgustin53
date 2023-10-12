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
    public bool gameOver { get; private set; }
 

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
        if (isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 2.0f);
            GameManager.AddToScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground" && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }
}