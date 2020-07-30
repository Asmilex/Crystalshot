using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

//
// ────────────────────────────────────────────────────────────── PROPIEDADES ─────
//
    public const int bounces_total = 3;
    public int bounces_left;

    public float speed = 20f;


//
// ────────────────────────────────────────────────────────────────── DESPAWN ─────
//

    public bool enable_despawn = false;
    public const float despawn_time = 5f;
    public float despawn_remaining_time;


//
// ────────────────────────────────────────────────────────────────── OBJETOS ─────
//

    public Rigidbody2D rb;
    private Vector2 ultima_velocidad;
    public GameObject impact_effect;
    private GameObject shooter;


//
// ──────────────────────────────────────────────────────────────── FUNCIONES ─────
//


    // Start is called before the first frame update
    void Start()
    {
        despawn_remaining_time = despawn_time;
        bounces_left           = bounces_total;

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        // Deshabilitar impacto con tu propio escudo
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shooter.GetComponent<PlayerController>().shield.GetComponent<Collider2D>());
    }

//
// ─────────────────────────────────────────────────────────────── COLISIONES ─────
//

    void OnCollisionEnter2D (Collision2D hit_info) {
        if (hit_info.gameObject.CompareTag("Wall")) {
            if (bounces_left > 0) {
                Vector2 _wallNormal = hit_info.GetContact(0).normal;
                Vector2 direction = Vector2.Reflect(ultima_velocidad, _wallNormal).normalized;

                transform.Rotate(0, 0, Vector2.SignedAngle(ultima_velocidad, direction));
                rb.velocity = direction * speed;

                bounces_left--;
            }
            else if (bounces_left == 0) {
                enable_despawn = true;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        else if (hit_info.gameObject.GetInstanceID() == shooter.GetInstanceID()) {
            if (shooter.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets) {
                Destroy(gameObject);
                shooter.GetComponent<PlayerController>().bullets_avaliable++;
            }
        }
    }
    void FixedUpdate() {
        if (enable_despawn) {
            despawn_remaining_time -= Time.deltaTime;
        }
        if (despawn_remaining_time <= 0) {
            Destroy(gameObject);

            if (shooter.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets)
                shooter.GetComponent<PlayerController>().bullets_avaliable++;
        }
    }

    void Update() {
        ultima_velocidad = rb.velocity;
    }
    public void assign_parent_id(GameObject padre) {
        shooter = padre;
    }
}
