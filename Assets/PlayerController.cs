using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    public LayerMask groundLayer;

    [Header("Horizontal Movement")]
    [Range(15, 35)]
    public float moveSpeed = 20f;
    public Vector2 direction;

    [Header("Jump")]
    [Range(10, 15)]
    public float jumpSpeed = 12f;
    public bool isJumping = false;

    public Transform shield;

    [Header("Physics")]
    public float maxSpeed = 10f;
    public float linearDrag = 7f;
    public float gravity = 3;
    public float fallMultiplier = 5;

    [Header("Collision")]
    public bool onGround = false;


    [SerializeField]
    public const int max_health = 3;
    public int health = max_health;

    public const int max_bullets = 3;
    public int bullets_avaliable;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 RJoystick;
    private Input_Player actions;



    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        bullets_avaliable = max_bullets;
    }

    private void Awake() {
        actions = new Input_Player();
        actions.Cube.Jump.started += ctx => OnJumpStart();
        actions.Cube.Jump.canceled += ctx => OnJumpStop();
    }

    private void OnJumpStart() {
        isJumping = true;
    }

    private void OnJumpStop() {
        isJumping = false;
    }

    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }

    private void FixedUpdate() {
        modifyPhysics();
    }

    private void Update() {
        Walk();

        // Ground check
        onGround = rb2d.IsTouchingLayers(groundLayer);

        // Read controls
        direction = actions.Cube.Movement.ReadValue<Vector2>();
        RJoystick = actions.Cube.RJoystick.ReadValue<Vector2>();

        // Shield
        Rotate_shield();
        shield.position = transform.position;

        // Jumps
        if(actions.Cube.Jump.triggered && onGround) {
            Jump();
        }
    }

    public bool Shooting_button_pressed() {
        return actions.Cube.Shoot.triggered;
    }

    public void Damage_taken() {
        if (health > 0) {
            // TODO Animación de impacto
            health--;
        }

        if (health == 0) {
            Destroy(gameObject);
            // TODO animación de haber si me muero
        }
    }


    private void Rotate_shield() {
        var new_angle = Vector2.Angle(RJoystick, Vector2.up);

        if (RJoystick.x > 0) {
            new_angle = -new_angle;
        }

        shield.eulerAngles = new Vector3(shield.eulerAngles.x, shield.eulerAngles.y, new_angle);
    }

    private void Walk() {
        rb2d.AddForce(Vector2.right * moveSpeed * direction.x);

        // Max speed
        if(Mathf.Abs(rb2d.velocity.x) > maxSpeed) {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
    }

    private void Jump() {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void modifyPhysics() {
        bool changingDirections = (direction.x > 0 && rb2d.velocity.x < 0) || (direction.x < 0 && rb2d.velocity.x > 0);

        // Horizontal movement
        if(onGround) {
            if(Mathf.Abs(direction.x) < 0.4f || changingDirections) {
                rb2d.drag = linearDrag;
            }
            else {
                rb2d.drag = 0f;
            }
            rb2d.gravityScale = 0;
        }
        // Jump
        else {
            rb2d.gravityScale = gravity;
            rb2d.drag = linearDrag * 0.15f;
            if(rb2d.velocity.y < 0) {
                rb2d.gravityScale = gravity * fallMultiplier;
            }
            else if(rb2d.velocity.y > 0 && !isJumping) {
                rb2d.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
}
