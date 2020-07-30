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
    Transform groundCheck;

//
// ─────────────────────────────────────────────────────────────────── INPUTS ─────
//
    private Vector2 input_vector = new Vector2(0, 0);
    // BA => Button Activated
    private bool jump_BA;
    private bool dash_BA;
    private bool shoot_BA;



    public const int max_bullets = 3;
    public int bullets_avaliable;


    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        bullets_avaliable = max_bullets;
    }

    void OnCollisionEnter2D(Collision2D collision) {

    }

    void OnCollisionExit2D(Collision2D collision) {

    }

    private void FixedUpdate() {

    }

    private void Update() {
        Walk();
    }

    public void Movement_handler(InputAction.CallbackContext context) {
        input_vector = context.ReadValue<Vector2>();
        Debug.Log("Valores (x, y) :" + input_vector.x.ToString() + ", " + input_vector.y.ToString());
    }

    public void Jump_handler(InputAction.CallbackContext action) {
        jump_BA = action.performed;
        Debug.Log("Salto pulsado: " + action.performed);
    }

    public void Dash_handler(InputAction.CallbackContext action) {
        dash_BA = action.performed;
        Debug.Log("Dash pulsada: " + action.performed);
    }
    public void Shoot_handler(InputAction.CallbackContext action) {
        shoot_BA = action.performed;
        Debug.Log("Shoot pulsada: " + action.performed);
    }


    private void Walk() {
        rb2d.velocity = new Vector2(moveSpeed * input_vector.x, rb2d.velocity.y);
    }
}
