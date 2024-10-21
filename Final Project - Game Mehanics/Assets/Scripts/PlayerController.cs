using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    
    private Rigidbody playerRb;

    private Animator playerAnim;
    
    private AudioSource playerAudio;
    
    public ParticleSystem explosionParticle;
    
    public ParticleSystem dirtParticle;
    
    public AudioClip jumpSound;
    
    public AudioClip crashSound;
    
    public float jumpForce = 10;
    
    public float gravityModifier;
    
    public bool isOnGround = true;
    
    public int appleCounter = 0;
    
    public bool gameOver;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
    
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }

    void Update()
    {
        if (!gameOver && Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            Jump();
        }

        if (appleCounter >= 5 && !gameOver)
        {
            gameOver = true;

            Time.timeScale = 0f;

            playerAnim.SetBool("isRunning", false);

            playerAnim.SetBool("isJumping", false);

            gameManager.ShowGameCompletedText();

            gameManager.GameOver();
        }
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isOnGround = false;

        playerAnim.SetTrigger("Jump_trig");

        dirtParticle.Stop();

        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;

            playerAnim.SetBool("Death_b", true);
            
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticle.Play();

            dirtParticle.Stop();
            
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            appleCounter++;

            gameManager.UpdateScore(1);
            
            Destroy(other.gameObject);
        }
    }
}