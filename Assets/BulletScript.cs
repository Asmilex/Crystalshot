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
    public const float despawn_time = 8f;
    public float despawn_remaining_time;


//
// ────────────────────────────────────────────────────────────────── OBJETOS ─────
//

    public Rigidbody2D rb;
    private Vector2 ultima_velocidad;
    public GameObject impact_effect;
    public GameObject shooter;


//
// ──────────────────────────────────────────────────────────────── FUNCIONES ─────
//


    // Start is called before the first frame update
    void Start()
    {
        despawn_remaining_time = despawn_time;
        bounces_left = bounces_total;

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
                enable_despawn = true;          // Check FixecUpdate to control the despawn timings
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        else if (shooter != null && hit_info.gameObject.GetInstanceID() == shooter.GetInstanceID()) {
            if (shooter.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets) {
                Destroy(gameObject);
                shooter.GetComponent<PlayerController>().bullets_avaliable++;
                shooter.GetComponent<PlayerController>().ASPick_Ammo.Play();
            }

            // NOTE Ojo, la bala tiene físicas e interaccionará con el jugador cuando esté inactiva.
            // Considerar un cambio de capa cuando esté en este estado con físicas desactivadas. Pero esto puede
            // producir otras consecuencias. Hay que tener ojo.
        }
        else if (hit_info.gameObject.CompareTag("Player")) {                            // Last condition ensures it is a different player
            if (rb.bodyType != RigidbodyType2D.Static) {                                // Bullet moving => Apply damage
                hit_info.gameObject.GetComponent<PlayerController>().Damage_taken();

                shooter.GetComponent<PlayerController>().bullets_avaliable
                  = (shooter.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets)
                  ? shooter.GetComponent<PlayerController>().bullets_avaliable + 1
                  : PlayerController.max_bullets;
                Debug.Log("He impactado en " + hit_info);

                Destroy(gameObject);
                // TODO Animación
            }
            else {                                                                    // Bullet stuck on wall => check if you can pick it up
                if (hit_info.gameObject.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets) {
                    shooter.GetComponent<PlayerController>().ASPick_Ammo.Play();
                    hit_info.gameObject.GetComponent<PlayerController>().bullets_avaliable++;
                }

                // Destroy the bullet anyway
                Destroy(gameObject);
                shooter.GetComponent<PlayerController>().Add_bullet_to_CD();
                // FIXME - ojo, esta última línea me huele raro. Posiblemente haya que reworkear la lógica.
            }
        }
        else if ( hit_info.collider.gameObject.layer == LayerMask.NameToLayer("Shield_active")
                 && hit_info.collider.gameObject.GetComponent<ShieldManager>().player.GetInstanceID() != shooter.GetInstanceID() )
        {
            // Bullet was parried => return it to sender!

            hit_info.gameObject.GetComponent<ShieldManager>().player.GetComponent<Weapon>().shoot_from_parry(bounces_left);
            shooter.GetComponent<PlayerController>().Add_bullet_to_CD();
            Destroy(gameObject);
        }
    }


    void FixedUpdate() {
        if (enable_despawn) {
            despawn_remaining_time -= Time.deltaTime;

            if (shooter == null || (shooter != null && shooter.GetComponent<PlayerController>().bullets_avaliable == PlayerController.max_bullets)) {
                // Transition to inactive

                // FIXME cambiar animación
            }
        }
        if (despawn_remaining_time <= 0) {
            Destroy(gameObject);

            if (shooter != null && shooter.GetComponent<PlayerController>().bullets_avaliable < PlayerController.max_bullets)
                shooter.GetComponent<PlayerController>().bullets_avaliable++;
        }

        // Check if player has max bullets while one of his is stuck on the wall
        if (rb.bodyType == RigidbodyType2D.Static
            && (shooter != null && shooter.GetComponent<PlayerController>().bullets_avaliable == PlayerController.max_bullets) ){

            // TODO - Transicionar a animación de bala neutra
            // TODO - Transicionar a cooldown de despawn neutro
        }
    }

    public void shooter_has_died() {
        // Hacer estático, quitar rebotes, quitarle el shooter y pasar a modo despawn
        bounces_left = 0;
        shooter = null;

    }

    void Update() {
        ultima_velocidad = rb.velocity;
    }
    public void assign_parent_id(GameObject padre) {
        shooter = padre;
    }
}
