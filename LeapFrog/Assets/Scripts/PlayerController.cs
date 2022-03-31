using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private StackController stackController;

    [SerializeField] private GameObject benzo;
    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] public Transform playerTransform;
    private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    bool isGrounded = true;
    [SerializeField] private Animator anim;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private int maxHealth;
    int currentNumJumps = 1;
    public int totalNumJumps = 1;
    private float squishLeeway = 1.2f;
    public List<GameObject> stack
    {
        get
        {
            return stackController.stack;
        }
    }

    private void Start()
    {
        anim.enabled = false;
        stackController.stack.Add(benzo);
        mainSpriteRenderer = benzo.GetComponent<SpriteRenderer>();
        stackController.topOfStack = new Vector3(0, benzo.GetComponent<BoxCollider2D>().bounds.size.y * 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Move the player left
        if(Input.GetKey(KeyCode.A))
        {
            mainRigidBody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = true;
            anim.enabled = true && isGrounded;
        }

        //Move the player right
        if(Input.GetKey(KeyCode.D))
        {
            mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = false;
            anim.enabled = true && isGrounded;
        }

        //Jump
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && currentNumJumps != 0)
        {
            mainRigidBody.AddForce(new Vector2(0 , jumpHeight));
            isGrounded = false;
            currentNumJumps--;
            anim.enabled = false;
        }

        //Pause the walking animation if the player is standing still or jumping
        if(Mathf.Abs(mainRigidBody.velocity.x) == 0)
        {
            anim.enabled = false;
        }

        //Flip allies
        flipAllies();
    }

    void Awake()
    {
        instance = this;
        Health = maxHealth;
    }

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            gameUI.showHealthFraction((float)Health / (float)maxHealth);
            if(health <= 0)
            {
                LoadingScreen.LoadScene("MainMenu");            }
        }
    }

    private float lastHitTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the player is touching the floor
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            if ((playerTransform.position.y > collision.gameObject.transform.position.y) &&
               Math.Abs(playerTransform.position.x - collision.gameObject.transform.position.x) < (collision.gameObject.GetComponent<Collider2D>().bounds.size.x * squishLeeway * 0.5))
            {
                isGrounded = true;
                currentNumJumps = totalNumJumps;
            }
        }
    }

    public void addAlly(GameObject ally)
    {
        stackController.addAlly(ally);
    }

    public void removeAlly(int i)
    {
        stackController.removeAlly(i);
    }

    //Flips an ally/allies to whichever direction the player is facing
    public void flipAllies() {
        bool flipped = mainSpriteRenderer.flipX;
        if(flipped == true && stack.Count > 1)
        {
            for(int i = 1; i < stack.Count; i++)
            {
                stackController.stack[i].GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if(flipped == false && stack.Count > 1)
        {
            for(int i = 1; i < stack.Count; i++)
            {
                stackController.stack[i].GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
