using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Rigidbody2D RB;
    public float walkSpeed = 10;
    [Range(1, 10)]
    public float acceleration = 1;//how quickly you accelerate when changing direction or starting movement
    [SerializeField] private LayerMask WhatIsGround;
    public List<float> jumpHeight = new List<float>();//list of heights of jumps. Allows for as many jumps as you want
    private int Jumps;//number of jumps the player currently has left.
    public bool variableJump = false;//can the player vary the height of jumps by releasing the jump button?
    [Range(1, 2)]
    public float fallingMult = 1;//how much faster you fall after reaching peak height or releasing jump button (if variableJump)
    [Range(0, 1)]
    public float airFriction = 0;
    public float waterGravMult = 0.5f;
    public float waterFriction = 0.5f;
    [SerializeField] private LayerMask WhatIsWater;
    private Transform Ground;//
    private Transform Ceiling;//Ground and ceiling checks
    private bool bGrounded;//grounded?
    private float GroundRadius = 0.2f;//radius for which it will become grounded
    private List<float> jumpSpeed = new List<float>();//list of jumpspeeds filled in when the game starts with a program. Guarantees constant jump height
    [SerializeField] private LayerMask WhatHurts;
    float initGrav;//initial gravity has to be stored somewhere
    public bool hashealth = false;
    public bool haslives = false;
    public int health = 1;
    public int lives = 3;
    public float invincibilityTime = 1;
    float invincibility = 0;
    bool inWater = false;
    int currentHealth;
    int currentLives;

    // Use this for initialization
    void Start () {
        jumpSpeed.Clear();
        jumpSpeed.TrimExcess();
        setJumpSpeed(0, jumpHeight.Count);
        //fill in jumpspeed list with all the values needed.
        initGrav = RB.gravityScale;
        Jumps = jumpSpeed.Count;
        currentHealth = health;
        currentLives = lives;
        //just storing initial values

    }
    public void setJumpSpeed(int first, int last)
    {
        for(int i = first; i < last; i++)
        {
            jumpSpeed.Add(Mathf.Sqrt(2 * RB.gravityScale * jumpHeight[i]));
        }
    }
    private void Awake()
    {
        Ground = transform.Find("Ground");
        Ceiling = transform.Find("Ceiling");
        //find these
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        bGrounded = false;
        if(Jumps == jumpSpeed.Count)
        {
            Jumps--;
        }
        //basically says that number of jumps is jumpSpeed.Count - 1 so that if the player runs off a platform, they don't get the grounded jump
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Ground.position, GroundRadius, WhatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != gameObject)
            {
                bGrounded = true;
                Jumps = jumpSpeed.Count;
            }
        }
        if (invincibility > 0)
        {
            invincibility -= Time.fixedDeltaTime;
        }
        //if grounded, get all jumps back
    }
    public void Move(float speed, bool jump, bool crouch)
    {
        speed *= walkSpeed;
        //speed is horizontal axis, so multiply by speed. Desired speed, not absolute.
        if (speed != 0)
        {
            if (RB.velocity.x > speed)
            {
                speed = RB.velocity.x - acceleration;
            }
            if (RB.velocity.x < speed)
            {
                speed = RB.velocity.x + acceleration;
            }
        }
        else
        {
            if(RB.velocity.x > 0)
            {
                speed = RB.velocity.x > acceleration ? RB.velocity.x - acceleration : 0;
            }
            if(RB.velocity.x < 0)
            {
                speed = Mathf.Abs(RB.velocity.x) > acceleration ? RB.velocity.x + acceleration : 0;
            }
        }
        //acceleration stuff. Makes sure it stops when done accelerating, and accelerates toward desired speed.
        if (!bGrounded && (RB.velocity.y < 0 || (variableJump && !Input.GetButton("Jump"))))
        {
            RB.gravityScale = fallingMult * initGrav * (inWater ? waterGravMult : 1);
        }
        //if it's in the air and it's either falling or the jump is variable and the player has released jump, fall faster
        else
        {
            RB.gravityScale = initGrav * (inWater ? waterGravMult : 1);
        }
        //if it's grounded or moving upward or the jump isn't variable or the player hasn't released jump, set gravityScale back to initial value
        RB.velocity = new Vector2(speed, RB.velocity.y);
        //set horizontal velocity to speed.
        if (jump && Jumps > 0)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpSpeed[jumpSpeed.Count - Jumps]);
            Jumps--;
            //if the player tried to jump and you can jump (Jumps counts jumps left) set the velocity to the jumpsSpeed's value at the jump the player's currently on and lose a jump.
        }
        RB.drag = inWater ? waterFriction : airFriction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((WhatIsWater.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            inWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((WhatIsWater.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            inWater = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((WhatHurts.value & 1 << col.gameObject.layer) == 1 << col.gameObject.layer
           && invincibility <= 0)
        {
            if (hashealth)
            {
                currentHealth--;
            }
            if(health <= 0 || !hashealth)
            {
                invincibility = invincibilityTime;
                currentLives--;
                currentHealth = health;
            }
            if(currentLives == 0)
            {
                die();
            }
        }
    }
    private void die()
    {
        //input dying code here.
    }
}

