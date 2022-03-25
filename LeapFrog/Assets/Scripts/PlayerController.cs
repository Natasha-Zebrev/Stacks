using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject benzo;
    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] private Transform playerTransform;
    private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    bool isGrounded = true;
    [SerializeField] private Animator anim;
    private List<GameObject> stack = new List<GameObject>();

    private void Start()
    {
        anim.enabled = false;
        stack.Add(benzo);
        mainSpriteRenderer = benzo.GetComponent<SpriteRenderer>();
}
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mainRigidBody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = true;
            anim.enabled = true && isGrounded;
        }

        if (Input.GetKey(KeyCode.D))
        {
            mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = false;
            anim.enabled = true && isGrounded;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded==true)
        {
            mainRigidBody.AddForce(new Vector2(0 , jumpHeight));
            isGrounded = false;
            anim.enabled = false;
        }

        if(Mathf.Abs(mainRigidBody.velocity.x) == 0)
        {
            anim.enabled = false;
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

    public void addAlly(GameObject enemy)
    {
        GameObject newAlly = Instantiate(enemy, playerTransform, false);
        float allyHeight = newAlly.GetComponent<BoxCollider2D>().bounds.size.y;
        //playerTransform.Translate(new Vector3(0, allyHeight, 0));
        newAlly.transform.Translate(new Vector3(0, allyHeight * stack.Count, 0));
        stack.Add(newAlly);
    }
}
