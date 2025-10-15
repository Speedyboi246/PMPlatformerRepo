using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    bool grounded = false;
    //where do we want to play the sound
    AudioSource audioSource;
    //what sound do we want to play when we jump
    public AudioClip jumpsound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //when we press left or right, move the char left/right
        float moveX = Input.GetAxis("Horizontal");
        //maintain the integrity of our Y velocity
        Vector3 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;
        //if you press space AND you're on the ground, jump the char
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //play my jump sound
            if (audioSource != null && jumpsound != null)
            {
                //play the jump sound
                audioSource.PlayOneShot(jumpsound);
            }
            rb.AddForce(new Vector2(0, 100 * jumpSpeed));
        }
    }
    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if(Collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
