using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;

    [Range(1,10)]
    public float moveSpeed = 3;

    [Range(1, 10)]
    public float JumpSpeed = 500f;

    [SerializeField]
    public Transform shield;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 input_vector = new Vector2(0, 0);
    private Vector2 RJoystick = new Vector2(0, 0);
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

    }

    private void Update() {
        Walk();
        input_vector = actions.Cube.Movement.ReadValue<Vector2>();
        RJoystick = actions.Cube.RJoystick.ReadValue<Vector2>();


        Rotate_shield();
        shield.position = transform.position;

        Debug.Log("RJ: (" + RJoystick.x + ", " + RJoystick.y + ")");
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
        rb2d.velocity = new Vector2(moveSpeed * input_vector.x, rb2d.velocity.y);
    }
}
