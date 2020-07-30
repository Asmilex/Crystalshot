using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform fire_point;
    public GameObject bullet_prefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start() {

    }

    void Update() {
        if (Shoot_button_pressed() && player.GetComponent<PlayerController>().bullets_avaliable > 0) {
            if (!Firepoint_inside_ground() && !Firepoint_inside_wall()) {
                var bala = shoot();
                bala.SendMessage("assign_parent_id", player);
                player.GetComponent<PlayerController>().bullets_avaliable--;
            }
        }
    }

    GameObject shoot() {
        return Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
    }

    bool Firepoint_inside_wall() {
        // FIXME - Cambiar la dirección del disparo con el input
        return Physics2D.Raycast(fire_point.position, Vector2.right, 0.1f, LayerMask.NameToLayer("Wall")).collider;
    }

    bool Firepoint_inside_ground() {
        // FIXME - Cambiar la dirección con el input
        RaycastHit2D rayo = Physics2D.Raycast(player.GetComponent<Transform>().position, Vector2.right, 1f, 1 << LayerMask.NameToLayer("Ground"));

        return rayo.collider;
    }

    public bool Shoot_button_pressed() {
        return player.GetComponent<PlayerController>().Shooting_button_pressed();
    }

}
