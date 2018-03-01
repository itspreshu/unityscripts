/*
    Basic 2D platform Character manager.
    Author: Preshu (Luis Ponce).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float CharacterSpeedNormalize;
    public float CharachterMaxSpeed;
    public float CharacterJumpForce;
    bool isFacingRight;
    int jumps = 2;
    public Vector3 punchDirection;

    void Start()
    {
        //Get the components and set properties at the start of the script.
        //Get RigidBody2D
        rb = GetComponent<Rigidbody2D>();
        //Get animator
        anim = GetComponentInChildren<Animator>();
        //Set orientation to right
        isFacingRight = true;
    }
    
    void FixedUpdate()
    {
        //If the player press horizontal axis. set animation to running.
        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isRunning", true);
            //If player move to right
            if (Input.GetAxis("Horizontal") > 0)
            {
                //Translate GameObject (Horizontal)
                transform.Translate(new Vector3(1 * Time.deltaTime * CharacterSpeedNormalize, 0, 0), Space.Self);
                //If the character is facing left, turn it to the right.
                if (!isFacingRight)
                {
                    isFacingRight = true;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
            }
            //If player move to left.
            if(Input.GetAxis("Horizontal") < 0)
            {
                //Translate GameObject (Horizontal)
                transform.Translate(Vector3.left * CharacterSpeedNormalize * Time.deltaTime, Space.Self);
                //If is facing right, turn it to the left.
                if (isFacingRight)
                {
                    isFacingRight = false;
                    transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
                }
            }
             
            
            
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        //If player press jump add force to the rigidbody2d and count Jumps
        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            rb.velocity = new Vector2(0, CharacterJumpForce);
            jumps--;

        }
        //TODO Shoot especific weapon.
        //TODO Take Damage.
        //TODO PowerUps.
        //TODO Character's hability.
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //If character is touching ground set bool value "isGrounded" to true and set rotation relative to the platform.
        if(collision.gameObject.tag == "Ground" && this.enabled)
        {
            anim.SetBool("isGrounded", true);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, collision.transform.rotation.z, transform.rotation.w);
        }

        if(collision.gameObject.tag == "Pushable" && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pushing");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(isFacingRight ? punchDirection : new Vector3(-punchDirection.x,punchDirection.y), ForceMode2D.Impulse);
            collision.gameObject.GetComponent<ItemBox>().PlayPunch();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If character touches ground, reset Jumps.
        if (collision.gameObject.tag == "Ground")
        {
            jumps = 2;
        }
        if (collision.gameObject.tag == "Ground" && this.enabled)
        {
            anim.SetBool("isGrounded", true);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, collision.transform.rotation.z, transform.rotation.w);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //If character leaves ground set "isGrounded" to false in animator.
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", false);
        }
    }
}
