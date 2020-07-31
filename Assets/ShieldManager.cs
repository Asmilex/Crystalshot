﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public GameObject player;
    public float parry_window = 0.3f;
    private float parry_left = 0;
    private bool was_active = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Shield_button_pressed() && !was_active) {
            activate_shield();
        }

        if (parry_left > 0) {
            parry_left -= Time.deltaTime;
            was_active = true;
        }

        if (was_active && parry_left < 0) {
            was_active = false;
            deactivate_shield();
        }
    }

    public void activate_shield() {
        gameObject.layer = LayerMask.NameToLayer("Shield_active");
        parry_left = parry_window;
        Debug.Log("Shield activated by " + player.ToString());
    }

    void deactivate_shield() {
        gameObject.layer = LayerMask.NameToLayer("Shield_inactive");
        Debug.Log("Shield deactivated by " + player.ToString());
    }

    public bool Shield_button_pressed() {
        return player.GetComponent<PlayerController>().Shield_button_pressed();
    }
}