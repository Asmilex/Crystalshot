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

    public Rigidbody2D rb;
    public GameObject impact_effect;

//
// ────────────────────────────────────────────────────────────────── DESPAWN ─────
//

    public bool enable_despawn = false;
    public const float despawn_time = 3f;
    public float despawn_remaining_time;

//
// ──────────────────────────────────────────────────────────────── FUNCIONES ─────
//


    // Start is called before the first frame update
    void Start()
    {
        despawn_remaining_time = despawn_time;
        bounces_left = bounces_total;

        rb.velocity = transform.right * speed;

        // FIXME - Cambiar cuando se cumplan las condiciones de despawneo. Mirar Máquina Estados Finitos.
        enable_despawn = true;
    }

//
// ─────────────────────────────────────────────────────────────── COLISIONES ─────
//
    /*
        Si es un jugador -> destruir la bala. Se hace en OnTriggerEnter2D, ya que no se hará nada más.
        Si es pared -> Mirar rebotes. Se hace en OnColliderEnter2D.
    */

    void OnTriggerEnter2D(Collider2D hit_info) {
        Debug.Log("Colisión detectada");

        // FIXME - Determinar el nombre del jugador
        /* Player player = hit_info.GetComponent<Player>();

        if (player != null) {
            player.take_damage(1);

            Instantiate(impact_effect, transform.position, transform.rotation);

            Destroy(gameObject);
        } */

    }

    void OnColliderEnter2D(Collision2D hit_info) {
        //Debug.Log("Colisión detectada");

        /* Pared pared = hit_info.collider.GetComponent<Pared>();

        if (pared != null) {
            var bounce_direction = Vector3.Reflect(rb.velocity.normalized, hit_info.GetContact(0).normal);
            rb.velocity = bounce_direction * Mathf.Max(speed, 0f);
        }*/
    }

    void FixedUpdate() {
        if (enable_despawn) {
            despawn_remaining_time -= Time.deltaTime;
        }
        if (despawn_remaining_time <= 0) {
            Destroy(gameObject);
        }
    }
}
