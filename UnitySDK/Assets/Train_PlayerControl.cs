using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_PlayerControl : MonoBehaviour
{

    public float Speed = 5f;
    public float DashSpeed = 10f;
    private float FinalSpeed;
    public float GroundMultiplier = 0.5f;

    public float heightChecker = 1.00f;

    public float JumpSpeed = 5f;
    public float DashJumpSpeed = 7f;
    public float WallMultiplier = 0.75f;
    private float FinalJumpSpeed;

    private bool IsGrounded = true;
    private bool IsWalled = false;
    bool WallJump = false;

    Vector3 input = new Vector3();
    private bool sButton = false;

    public float GroundAccel = 10.0f;
    public float AirAccel = 8.0f;

    Vector3 JumpDirection = Vector3.up;

    public Rigidbody rb;

    private int lookDirection = -1;
    private int jumping = 0;
    public bool ToDie = true;

    private float counter = 0.0f;

    private bool DeathState;
    private Vector3 RegularScale;
    private Vector3 ScaleVariable;

    //AudioSource jumpingSound;
    //AudioSource deathSound;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        JumpSpeed = 5.0f;

        RegularScale = this.transform.localScale;
        ScaleVariable = RegularScale;

        //AudioSource[] sources = this.GetComponents<AudioSource>();

       // jumpingSound = sources[0];
        //deathSound = sources[1];

    }

    // Update is called once per frame
    void Update()
    {


        if (!DeathState)
        {
            StopAllCoroutines();
            rb.isKinematic = false;
            input = Vector3.zero;
            //input.x = Input.GetAxis("Horizontal");

            IsGrounded = OnGround();
            Debug.Log("Ground: " + IsGrounded);

            IsWalled = OnWall();
            Debug.Log("Wall: " + IsWalled);

            //if (Input.GetKeyDown(KeyCode.Joystick1Button13) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4)) sButton = true;
            //if (Input.GetKeyUp(KeyCode.Joystick1Button13) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.JoystickButton4)) sButton = false;

            if (!sButton)
            {
                FinalSpeed = Speed;
                FinalJumpSpeed = JumpSpeed;
            }
            else
            {
                FinalSpeed = DashSpeed;
                FinalJumpSpeed = DashJumpSpeed;
            }

            if (input.x != 0.00f)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -lookDirection * 90.0f, transform.eulerAngles.z);
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button17) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                //jumping = 1;
                //characterAnimator.SetTrigger("Jumping");
            }

            // Debug.Log(input.x);

            if (input.x < 0.01f)
            {
                lookDirection = -1;
            }

            if (input.x > 0.01f)
            {
                lookDirection = 1;
            }

            if (OnWall())
            {
                int d = FindWallDirection();
                lookDirection = d;
            }
        }
        else
        {
            //if (deathSound.isPlaying == false) deathSound.Play();
            rb.isKinematic = true;
        }
    }

    // Physics updates happen here
    void FixedUpdate()
    {

        if (!DeathState)
        {


            rb.AddForce(new Vector3(((input.x * FinalSpeed) - rb.velocity.x) * (OnGround() ? GroundAccel : AirAccel), 0.0f, 0.0f));
            //rb.velocity = new Vector3((input.x == 0 && OnGround()) ? 0 : rb.velocity.x, (jumping == 1 && IsTouching()) ? JumpSpeed : rb.velocity.y);

            Vector3 v = Vector3.zero;

            if (input.x == 0 && OnGround())
            {
                v.x = 0;
            }
            else
            {
                v.x = rb.velocity.x;
            }

            if (jumping == 1 && IsTouching())
            {
                //jumpingSound.Play();
                v.y = FinalJumpSpeed;
            }
            else
            {
                v.y = rb.velocity.y;
            }

            rb.velocity = v;

            if (OnWall() && !OnGround() && jumping == 1)
            {
                Vector3 f = new Vector3(-FindWallDirection() * FinalJumpSpeed * WallMultiplier, rb.velocity.y, rb.velocity.z);
                rb.AddForce(f, ForceMode.Impulse);
                //jumping = 0;
            }

            if (OnGround() && !OnWall() && jumping == 1)
            {
                Vector3 f = new Vector3(rb.velocity.x, FinalJumpSpeed * GroundMultiplier, rb.velocity.z);
                rb.AddForce(f, ForceMode.Impulse);
            }

            if (jumping == 1) jumping = 0;
        }
    }

    bool OnGround()
    {
        //Cast a ray downwards to check if the player is on the ground.
        Collider self = gameObject.GetComponent<Collider>();
        float height = self.bounds.size.y;
        Ray CheckGround = new Ray(gameObject.transform.position, -Vector3.up);
        bool GroundHit = Physics.Raycast(CheckGround, heightChecker);
        Debug.DrawRay(CheckGround.origin, -Vector3.up * heightChecker, Color.yellow);
        return GroundHit;

    }

    bool OnWall()
    {
        //Cast a ray left and right of itself to check if the player is touching a wall.
        Collider self = gameObject.GetComponent<Collider>();
        float width = self.bounds.size.x;

        Ray LeftHit = new Ray(gameObject.transform.position, -Vector3.right);
        Ray RightHit = new Ray(gameObject.transform.position, Vector3.right);

        bool LHit = Physics.Raycast(LeftHit, (width + 0.05f)); //Check for later: Is the slight offset required?
        bool RHit = Physics.Raycast(RightHit, (width + 0.05f));

        Debug.DrawRay(LeftHit.origin, LeftHit.direction * (width + 00.05f), Color.red);
        Debug.DrawRay(RightHit.origin, RightHit.direction * (width + 00.05f), Color.blue);

        if (LHit || RHit) return true;
        else return false;
    }

    bool IsTouching()
    {
        if (OnGround() || OnWall())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int FindWallDirection()
    {
        int direction = 0;
        //Physics.gravity = new Vector3(0f, -1f, 0f);

        if (!IsWalled)
        {
            direction = 0;
        }
        else
        {
            Collider self = gameObject.GetComponent<Collider>();
            float width = self.bounds.size.x;

            Ray LeftHit = new Ray(gameObject.transform.position, -Vector3.right);
            Ray RightHit = new Ray(gameObject.transform.position, Vector3.right);

            bool LHit = Physics.Raycast(LeftHit, (width + 0.05f)); //Check for later: Is the slight offset required?
            bool RHit = Physics.Raycast(RightHit, (width + 0.05f));

            if (RHit)
            {
                direction = 1;
                //Physics.gravity = new Vector3(1.0f, 0f, 0f);
                Debug.Log("Wall on right");
            }

            if (LHit)
            {
                direction = -1;
                //Physics.gravity = new Vector3(-1.0f, 0f, 0f);
                Debug.Log("Wall on left");
            }
        }

        return direction;
    }

    public int GetWallDirection()
    {
        return FindWallDirection(); // returns 0 when on floor, 1 when on wall right and -1 when on wall left.
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            if (ToDie)
            {
                DeathState = true;
                //Respawn();
            }
        }
    }

    public bool IsDying()
    {
        if (DeathState)
        {
            return true;
        }
        else return false;
    }
    //functions below only for trainning
    public void move(float direction)
    {
        input.x = direction;
    }
    public void jump()
    {
        jumping = 1;
    }

}



/* 
void OnCollisionStay(Collision collision){
    IsGrounded = true;

    if(collision.gameObject.tag == "floor"){
        CanMove = true;
    }

    if(collision.gameObject.tag == "wall"){
    }

}

void OnCollisionExit(){
    IsGrounded = false;
    CanMove = true;
}



    if(collision.gameObject.tag == "wall"){
        Debug.Log("Hit a wall!");
        CanMove = false;
        rb.velocity = Vector3.zero;
        rb.angularDrag = 0.0f;
    }
}
*/
