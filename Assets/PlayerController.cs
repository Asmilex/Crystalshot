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
    public GameObject sprite;

    [Header("Horizontal Movement")]
    [Range(15, 75)]
    public float moveSpeed = 20f;
    public Vector2 direction;

    [Header("Jump")]
    [Range(10, 15)]
    public float jumpSpeed = 12f;
    public float jumpDelay = 0.15f;
    public bool isJumping = false;
    private float jumpTimer;
    private bool wasOnGround;

    [Header("Dash")]
    public bool dashAvailable = true;
    public Vector2 dashDirection;
    public float dashSpeed = 50f;
    public float dashDuration = 0.5f;
    public bool dashing = false;
    private float dashTimer;

    const float dash_time_reload = 0.5f;
    float dash_cooldown = 0;


    [Header("Shield")]
    public Transform shield;

    [Header("Physics")]
    public float maxSpeed = 10f;
    public float linearDrag = 7f;
    public float gravity = 3;
    public float fallMultiplier = 5;

    [Header("Collision")]
    public bool onGround = false;
    public bool onPlayer = false;

    [Header("Health")]
    public const int max_health = 3;
    public int health = max_health;

    [Header("Bullets")]
    public const int max_bullets = 3;
    public int bullets_avaliable;

    private float[] bullets_on_cooldown = new float [3] {0f, 0f, 0f};
    private int bullets_on_cooldown_size = 3;
    public float bullet_respawn_time = 5f;

    public GameObject controlador;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 RJoystick;

    public char color;
    //Sounds
    public AudioSource ASShoots;
    public AudioSource ASJump;
    public AudioSource ASReflect;
    public AudioSource ASPick_Ammo;
    public AudioSource ASDash;
    public AudioSource ASWalk;
    public AudioSource ASHit;


    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        bullets_avaliable = max_bullets;

        var aSources = GetComponents<AudioSource>();

        ASDash = aSources[0];
        ASPick_Ammo = aSources[1];
        ASReflect = aSources[2];
        ASShoots = aSources[3];
        ASJump = aSources[4];
        ASWalk = aSources[5];
        ASHit = aSources[6];

        controlador.GetComponent<Game_controller>().add_player(gameObject);

    }

    private void Awake() {

        // Jump events
    }

    private void OnJumpStart() {
        isJumping = true;
    }

    private void OnJumpStop() {
        isJumping = false;
    }

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(rb2d.IsTouchingLayers(playerLayer) && collision.gameObject.transform.position.y < rb2d.transform.position.y) {
            onPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        onPlayer = false;
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
        if(jumpTimer > Time.time && (onGround || onPlayer)) {
            Jump();
        }

        // Dash timer
        if (dash_cooldown > 0) {
            dash_cooldown -= Time.deltaTime;
            if (dash_cooldown < 0) {
                dash_cooldown = 0;
            }
        }

        if(dashTimer <= Time.time && dashing) {
            rb2d.velocity = Vector2.zero;
            dashing = false;
        }


        Walk();
    }

    private void Update() {

        // Ground check
        // FIXME: cuando dos jugadores colisionan en el aire pasan a tener gravedad cero
        wasOnGround = onGround || onPlayer;
        onGround = rb2d.IsTouchingLayers(groundLayer);

        if(onGround && dash_cooldown == 0) {
            dashAvailable = true;
        }

        // Ground squeeze
        if(!wasOnGround && (onGround || onPlayer)) {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        // Read controls
        // Shield
        Rotate_shield();
        shield.position = transform.position;
    }

    public void Damage_taken() {
        if (health > 0) {
            ASHit.Play();
            // TODO Animación de impacto
            health--;
        }

        controlador.GetComponent<Game_controller>().update_health(gameObject);
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
        if((Mathf.Abs(rb2d.velocity.x) > maxSpeed) && !dashing) {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
    }

    private void Jump() {
        ASJump.Play();
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;

        // Squeeze
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }

    private void Dash() {
        // Dash direction
        if(direction.x > 0 && direction.y > 0) {
            dashDirection = new Vector2(1,1) * (Mathf.Sqrt(2)/2) * dashSpeed;
        }
        else if(direction.x > 0 && direction.y == 0) {
            dashDirection = Vector2.right * dashSpeed;
        }
        else if(direction.x > 0 && direction.y < 0) {
            dashDirection = new Vector2(1,-1) * (Mathf.Sqrt(2)/2) * dashSpeed;
        }
        else if(direction.x < 0 && direction.y > 0) {
            dashDirection = new Vector2(-1,1) * (Mathf.Sqrt(2)/2) * dashSpeed;
        }
        else if(direction.x < 0 && direction.y == 0) {
            dashDirection = Vector2.left * dashSpeed;
        }
        else if(direction.x < 0 && direction.y < 0) {
            dashDirection = new Vector2(-1,-1) * (Mathf.Sqrt(2)/2) * dashSpeed;
        }
        else if(direction.x == 0 && direction.y > 0) {
            dashDirection = Vector2.up * dashSpeed;
        }
        else if(direction.x == 0 && direction.y < 0) {
            dashDirection = Vector2.down * dashSpeed;
        }

        // Dash movement
        rb2d.velocity = dashDirection;
        dash_cooldown = dash_time_reload;
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
        // Dash
        else if(dashing) {
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

    void OnMovement(InputValue valor) {
        direction = valor.Get<Vector2>();
    }

    void OnRJoystick(InputValue valor) {
        RJoystick = valor.Get<Vector2>();
    }

    void OnJump(InputValue valor) {
        jumpTimer = Time.time + jumpDelay;
    }

    void OnBlock(InputValue valor) {
        if (valor.Get<float>() == 1) {
            ASReflect.Play();
            gameObject.GetComponentInChildren<ShieldManager>().activate_shield();
        }
    }

    void OnDash(InputValue valor) {
        if (dashAvailable) {
            ASDash.Play();
            dashTimer = Time.time + dashDuration;
            dashAvailable = false;
            dashing = true;
            Dash();
        }
    }
}
