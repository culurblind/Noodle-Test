using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public Rigidbody2D body;
    public float groundSpeed;
    public float jumpSpeed;

    [Range(0f, 1f)]
    public float groundDecay;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float speedMultiplier;

    float xInput;
    float yInput;

    private bool isSpeedBoosted = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        getInput();
        handleJump();
    }

    void FixedUpdate() {
        CheckGround();
        friction();
        moveWithInput();
    }

    void getInput() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void moveWithInput() {
        if (Mathf.Abs(xInput) > 0){

            float increment = xInput * acceleration;
            float maxSpeed = isSpeedBoosted ? groundSpeed * 2 : groundSpeed;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxSpeed, maxSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            //float direction = Mathf.Sign(xInput);
            //transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void handleJump() {
        if (Input.GetButtonDown("Jump") && grounded) {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void friction() {
        if (grounded && xInput == 0 && yInput == 0) {
            body.velocity *= groundDecay;
        }
    }

    public void StartSpeedChanger(float multiplier, float duration)
    {
        StartCoroutine(SpeedChangerCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedChangerCoroutine(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
    }
}
