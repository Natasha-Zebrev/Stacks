using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] public StackController stackController;

    [SerializeField] private GameObject benzo;
    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] public Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Animator anim;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private AudioSource acquireAlly;
    [SerializeField] private AudioSource loseAlly;
    [SerializeField] private AudioSource sizzle;
    [SerializeField] private int maxHealth;

    private SpriteRenderer mainSpriteRenderer;
    bool isGrounded = true;
    public int currentNumJumps = 1;
    public int totalNumJumps = 1;
    public bool touchLava = false;
    public bool canJump = true;
    public bool controlsReversed = false;
    
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
        //Keeps the game from freezing after restarting from a menu
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the player left
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(controlsReversed)
            {
                mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
                mainSpriteRenderer.flipX = false;
            } else
            {
                mainRigidBody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
                mainSpriteRenderer.flipX = true;
            }

            anim.enabled = true && isGrounded;
            flipAllies();
        }

        //Move the player right
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (controlsReversed)
            {
                mainRigidBody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
                mainSpriteRenderer.flipX = true;
            }
            else
            {
                mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
                mainSpriteRenderer.flipX = false;
            }

            anim.enabled = true && isGrounded;
            flipAllies();
        }

        //Jump
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        && currentNumJumps != 0 && canJump)
        {
            mainRigidBody.AddForce(new Vector2(0 , jumpHeight));
            isGrounded = false;
            currentNumJumps--;
            anim.enabled = false;
            playerOnPlatform(false, null);
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("MainMenu");
        }

        //Pause the walking animation if the player is standing still or jumping
        if(Mathf.Abs(mainRigidBody.velocity.x) == 0)
        {
            anim.enabled = false;
        }

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
                benzo.SetActive(false);
                gameUI.winFailUI.fail();
            }
        }
    }

    private int stackHealth;
    public int StackHealth
    {
        get
        {
            return stackHealth;
        }
        set
        {
            stackHealth = value;
            gameUI.showStackHealth((float)Health / (float)maxHealth);
        }
    }

    private float lastHitTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool above = playerTransform.position.y > collision.gameObject.transform.position.y;
        float yPosDif = Math.Abs(playerTransform.position.y - collision.gameObject.transform.position.y);
        bool aboveThing = above && yPosDif >= (collision.collider.bounds.size.y + collision.otherCollider.bounds.size.y) / 2;
        //benzo.GetComponent<BoxCollider2D>().bounds.min.y > (collision.collider.bounds.max.y - collision.collider.bounds.size.y / 5);
        float xPosDif = Math.Abs(playerTransform.position.x - collision.gameObject.transform.position.x);
        bool onObstacle = aboveThing && xPosDif < (collision.collider.bounds.size.x + collision.otherCollider.bounds.size.x) / 2;

        if ((collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("GhostWall") || collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("Elevator") || (collision.gameObject.CompareTag("LavaWall") && stackController.containsAlly("DemonAlly")) || collision.gameObject.CompareTag("MushroomFloor") 
            || collision.gameObject.CompareTag("WinPlat")) && onObstacle)
        {
            isGrounded = true;
            currentNumJumps = totalNumJumps;
        }
        else if (collision.gameObject.CompareTag("LavaWall") && !touchLava)
        {
            if (Health > 0)
            {
                    sizzle.Play();
                    Health--;
                    Debug.Log(Health);
             }
            /* else if (stack.Count == 2)
             {

                 PlayerController.instance.removeAlly(stack.Count - 1);
             }
             else if (stack.Count > 2 && stack.Count <= 14)
             {
                 int numToRemove = (int)Mathf.Floor(Mathf.Sqrt(stack.Count - 2));
                 int stackCount = stack.Count;
                 for (int i = stackCount - 1; i > stackCount - 1 - numToRemove; i--)
                 {
                     PlayerController.instance.removeAlly(i);
                 }
             }
             else if (stack.Count > 14)
             {
                 int removeMax = 5;
                 for (int i = stack.Count - 1; i > stack.Count - 1 - removeMax; i--)
                 {
                     PlayerController.instance.removeAlly(i);
                 }
             }
            */
        }
        else if(collision.gameObject.CompareTag("MovingPlat") && onObstacle)
        {
            playerOnPlatform(true, collision);
            
        }
        else
        {
            playerOnPlatform(false, collision);
        }

        if(collision.gameObject.CompareTag("KillBox"))
        {
            gameUI.winFailUI.fail();
        }
    }

    private void playerOnPlatform(bool onPlatform, Collision2D platform)
    {
        if (onPlatform)
        {
            playerTransform.parent = platform.gameObject.transform;
            currentNumJumps = totalNumJumps;
            isGrounded = true;
        }
        else
        {
            playerTransform.parent = null;
        }
    }

    public void addAlly(GameObject ally)
    {
        stackController.addAlly(ally);
        gameUI.showStackHealth((float)(stack.Count - 1) / (float)gameUI.targetSize);
        acquireAlly.Play();
    }

    public void removeAlly(int i)
    {
        stackController.removeAlly(i);
        gameUI.showStackHealth((float)(stack.Count - 1) / (float)gameUI.targetSize);
        loseAlly.Play();
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

    public void removeFriction(bool friction)
    {
        if (friction)
            mainRigidBody.drag = 0;
        else
            mainRigidBody.drag = 1.2f;
    }
}
