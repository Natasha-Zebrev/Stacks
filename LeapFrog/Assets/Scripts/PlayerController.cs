using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject benzo;
    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] private Transform playerTransform;
    private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    bool isGrounded = true;
    [SerializeField] private Animator anim;
    public List<GameObject> stack = new List<GameObject>();
    [SerializeField] private GameUI gameUI;
    [SerializeField] private int maxHealth;
    public Vector3 topOfStack;

    private void Start()
    {
        anim.enabled = false;
        stack.Add(benzo);
        mainSpriteRenderer = benzo.GetComponent<SpriteRenderer>();
        topOfStack = new Vector3(0, benzo.GetComponent<BoxCollider2D>().bounds.size.y * 0.5f, 0);
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
        if (Input.GetKey(KeyCode.D))
        {
            mainRigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0));
            mainSpriteRenderer.flipX = false;
            anim.enabled = true && isGrounded;
        }

        //Jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded==true)
        {
            mainRigidBody.AddForce(new Vector2(0 , jumpHeight));
            isGrounded = false;
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
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        isGrounded = true;
    }

    //Turn a defeated enemy into an ally and add it to the player stack
    public void addAlly(GameObject enemy)
    {
        GameObject newAlly = Instantiate(enemy, playerTransform, false);
        float allyHeight = newAlly.GetComponent<BoxCollider2D>().bounds.size.y;
        //Code that places the enemy/newAlly under the player instead of on top
        //playerTransform.Translate(new Vector3(0, allyHeight, 0));
        topOfStack += new Vector3(0, allyHeight, 0);
        newAlly.transform.localPosition = topOfStack;
        topOfStack += new Vector3(0 , allyHeight * 0.5f, 0);
        stack.Add(newAlly);
    }

    //Flips an ally/allies to whichever direction the player is facing
    public void flipAllies() {
        bool flipped = mainSpriteRenderer.flipX;
        if (flipped == true && stack.Count > 1)
        {
            for(int i = 1; i < stack.Count; i++)
            {
                stack[i].GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (flipped == false && stack.Count > 1)
        {
            for(int i = 1; i < stack.Count; i++)
            {
                stack[i].GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
