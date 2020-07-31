using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controller : MonoBehaviour
{
    List<GameObject> jugadores = new List<GameObject>();

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
                    Debug.Log("F en el debug para " + jugador.ToString() );

                    var balas = GameObject.FindGameObjectsWithTag("Bullet");
                    for (int j = 0; j < balas.Length; j++) {
                        if ( balas[j].GetComponent<BulletScript>().shooter != null
                            && balas[j].GetComponent<BulletScript>().shooter.GetInstanceID() == jugador.GetInstanceID())
                        {
                            balas[j].GetComponent<BulletScript>().shooter_has_died();
                        }
                    }
                    // Decirle a las balas que han muerto
                    Destroy(jugador);
                    jugadores.Remove(jugadores[i]);
                }
                else {
                    jugadores[i] = jugador;
                }
            }
        }
    }
}
