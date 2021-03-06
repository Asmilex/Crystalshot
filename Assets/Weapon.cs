﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform fire_point;
    public GameObject bullet_prefab;
    public GameObject player;
    public AudioSource ASShoot;


    // Start is called before the first frame update
    void Start() {
        var aSources = GetComponents<AudioSource>();
        ASShoot = aSources[3];
    }

    void Update() {
    }

    GameObject shoot() {
        return Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
    }

    bool Can_be_fired() {
        var angle = player.GetComponent<PlayerController>().shield.eulerAngles.z + 90;
        var raycast_direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        Debug.DrawRay(player.GetComponent<Transform>().position, raycast_direction, Color.red, 3);

        RaycastHit2D rayo = Physics2D.Raycast(
              player.GetComponent<Transform>().position
            , raycast_direction
            , 1.5f
            , 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Wall")
        );

        return !rayo.collider;
    }

    public void shoot_from_parry(int bounces_left) {
        if (Can_be_fired()) {
            var bala = shoot();
            bala.GetComponent<BulletScript>().assign_parent_id(player);
            bala.GetComponent<BulletScript>().bounces_left = bounces_left;
        }
    }

    void OnShoot(InputValue valor) {
        if (player.GetComponent<PlayerController>().bullets_avaliable > 0) {
            if (Can_be_fired()) {
                ASShoot.Play();
                var bala = shoot();
                bala.GetComponent<BulletScript>().assign_parent_id(player);
                player.GetComponent<PlayerController>().bullets_avaliable--;
            }
        }
    }
}
