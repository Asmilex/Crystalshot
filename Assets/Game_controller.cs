using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Game_controller : MonoBehaviour
{


    List<(int, int)> jugadores = new List<(int, int)>(); // Stores (instanceID, health)
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void update_health((int, int) player_health) {
        for (int i = 0; i < jugadores.Capacity; i++) {
            if (player_health.Item1 == jugadores[i].Item1) {
                jugadores[i] = player_health;
            }
        }
    }
}
