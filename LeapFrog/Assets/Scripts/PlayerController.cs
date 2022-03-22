using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    bool isGrounded = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mainRigidBody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = false;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded==true)
        {
            mainRigidBody.AddForce(new Vector2(0 , jumpHeight));
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        isGrounded = true;
    }
}
