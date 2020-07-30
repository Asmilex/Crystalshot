﻿using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;

    [Header("Horizontal Movement")]
    [Range(15, 35)]
    public float moveSpeed = 20f;
    public Vector2 direction;

    [Header("Jump")]
    [Range(1, 10)]
    public float JumpSpeed = 500f;

    [SerializeField]
    public Transform shield;

    [Header("Physics")]
    public float maxSpeed = 10f;
    public float linearDrag = 7f;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 RJoystick;
    private Input_Player actions;

    public const int max_bullets = 3;
    public int bullets_avaliable;


    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        bullets_avaliable = max_bullets;
    }

    private void Awake() {
        actions = new Input_Player();
    }

    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }
    void OnCollisionEnter2D(Collision2D collision) {

    }

    void OnCollisionExit2D(Collision2D collision) {

    }

    private void FixedUpdate() {
        Walk();
        modifyPhysics();
    }

    private void Update() {
        Walk();
        direction = actions.Cube.Movement.ReadValue<Vector2>();
        RJoystick = actions.Cube.RJoystick.ReadValue<Vector2>();


        Rotate_shield();
        shield.position = transform.position;
    }

    public bool Shooting_button_pressed() {
        return actions.Cube.Shoot.triggered;
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

    private void modifyPhysics() {
        bool changingDirections = (direction.x > 0 && rb2d.velocity.x < 0) || (direction.x < 0 && rb2d.velocity.x > 0);

        if(Mathf.Abs(direction.x) < 0.4f || changingDirections) {
            rb2d.drag = linearDrag;
        }
        else {
            rb2d.drag = 0f;
        }
    }
}
