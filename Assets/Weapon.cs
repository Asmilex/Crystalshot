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
            var bala = shoot();
            bala.SendMessage("assign_parent_id", player);
            player.GetComponent<PlayerController>().bullets_avaliable--;
        }
    }

    GameObject shoot() {
        return Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
    }

    public bool Shoot_button_pressed() {
        return player.GetComponent<PlayerController>().Shooting_button_pressed();
    }

}
