﻿using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public SpriteRenderer sprite;

    [Header("Horizontal Movement")]
    [Range(15, 35)]
    public float moveSpeed = 20f;
    public Vector2 direction;

    [Header("Jump")]
    [Range(10, 15)]
    public float jumpSpeed = 12f;
    public float jumpDelay = 0.15f;
    public bool isJumping = false;
    private float jumpTimer;

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

    private float[] bullets_on_cooldown = new float [3] {0f, 0f, 0f};
    private int bullets_on_cooldown_size = 3;
    public float bullet_respawn_time = 5f;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 RJoystick;
    private Input_Player actions;



    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
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

        // Handle respawn in case there are any bullets on cooldown.
        // Check first if you shouldn't have any cooldowns active; that is, you have stolen a bullet
        if (bullets_avaliable == max_bullets) {
            for (int i = 0; i < bullets_on_cooldown_size; i++) {
                bullets_on_cooldown[i] = 0;
            }
        }
        // Otherwise decrease the remaining time accordingly if you have any on cooldown
        else {
            for (int i = 0; i < bullets_on_cooldown_size; i++) {
                if (bullets_on_cooldown[i] > 0) {
                    bullets_on_cooldown[i] -= Time.deltaTime;

                    if (bullets_on_cooldown[i] <= 0) {      // Once any of them reaches 0, update the bullet count if pertinent
                        bullets_avaliable = (bullets_avaliable < max_bullets) ? bullets_avaliable + 1 : max_bullets;
                        bullets_on_cooldown[i] = 0;
                    }
                }

            }
        }


        // Coyote time
        if(jumpTimer > Time.time && onGround) {
            Jump();
        }
    }

    private void Update() {
        Walk();

        // Ground check
        onGround = (rb2d.IsTouchingLayers(groundLayer)) || (rb2d.IsTouchingLayers(playerLayer));

        // Read controls
        direction = actions.Cube.Movement.ReadValue<Vector2>();
        RJoystick = actions.Cube.RJoystick.ReadValue<Vector2>();

        // Shield
        Rotate_shield();
        shield.position = transform.position;

        // Jump
        if(actions.Cube.Jump.triggered) {
            jumpTimer = Time.time + jumpDelay;
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

    public void Add_bullet_to_CD() {
        for (int i = 0; i < bullets_on_cooldown_size; i++) {
            if (bullets_on_cooldown[i] == 0) {
                bullets_on_cooldown[i] = bullet_respawn_time;
            }
        }
    }


    private void Rotate_shield() {
        if (RJoystick != Vector2.zero) {
            var new_angle = -Vector2.SignedAngle(RJoystick, Vector2.up);

            shield.eulerAngles = new Vector3(shield.eulerAngles.x, shield.eulerAngles.y, new_angle);
        }
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
        jumpTimer = 0;

        // Squeeze
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
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

    // Jump squeeze
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {
        Vector2 originalSize = Vector2.one;
        Vector2 newSize = new Vector2(xSqueeze, ySqueeze);
        
        float t = 0f;
        while(t <= 1.0) {
            t += Time.deltaTime / seconds;
            sprite.transform.localScale = Vector2.Lerp(originalSize, newSize, t);
            yield return null;
        }

        t = 0f;
        while(t <= 1.0) {
            t += Time.deltaTime / seconds;
            sprite.transform.localScale = Vector2.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }
}
