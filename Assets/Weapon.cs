using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform fire_point;
    public GameObject bullet_prefab;
    public GameObject player;



    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1") && player.GetComponent<PlayerController>().bullets_avaliable > 0) {
            shoot();
            player.GetComponent<PlayerController>().bullets_avaliable--;
        }
    }

    void shoot() {
        var bala = Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
        bala.SendMessage("assign_parent_id", player);
    }
}
