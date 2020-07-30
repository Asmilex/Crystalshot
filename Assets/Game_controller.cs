using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controller : MonoBehaviour
{


    List<GameObject> jugadores = new List<GameObject>(); // Stores (instanceID, health)
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void add_player(GameObject player) {
        jugadores.Add(player);
    }

    public void update_health(GameObject jugador) {
        for (int i = 0; i < jugadores.Count; i++) {
            if (jugadores[i].GetInstanceID() == jugador.GetInstanceID()) {
                if (jugadores[i].GetComponent<PlayerController>().health == 0) {
                    Destroy(jugador);
                    Debug.Log("F en el debug para " + jugador.ToString() );

                    jugadores.Remove(jugadores[i]);
                }
                else {
                    jugadores[i] = jugador;
                }
            }
        }
    }
}
