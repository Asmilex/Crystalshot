using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    List<GameObject> jugadores = new List<GameObject>();


    // P1
    public Image[] heartsP1;
    public Sprite fullLifeP1;
    public Sprite emptyLifeP1;

    // P2
    public Image[] heartsP2;
    public Sprite fullLifeP2;
    public Sprite emptyLifeP2;

    // P3
    public Image[] heartsP3;
    public Sprite fullLifeP3;
    public Sprite emptyLifeP3;

    // P4
    public Image[] heartsP4;
    public Sprite fullLifeP4;
    public Sprite emptyLifeP4;

    void Start()
    {
        Screen.SetResolution(2048,1024, true);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }

    public void add_player(GameObject player) {
        Debug.Log(player);
        jugadores.Add(player);
    }

    public void update_health(GameObject jugador) {
        for (int i = 0; i < jugadores.Count; i++) {
            if (jugadores[i].GetInstanceID() == jugador.GetInstanceID()) {

                Debug.Log(jugador.GetComponent<PlayerController>().color);
                //UpdateUI_Alt(jugador.GetComponent<PlayerController>().color, jugador.GetComponent<PlayerController>().health);

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

    void UpdateUI_Alt(char color, int vida_restante) {
        int jugador;
        Debug.Log(color);

        if (color == 'b') {
            jugador = 0;
        }
        else if (color == 'r') {
            jugador = 1;
        }
        else if (color == 'g') {
            jugador = 2;
        }
        else {
            jugador = 3;
        }

        if (vida_restante != 0) {
            Debug.Log("Jugador, vida " + jugador + vida_restante);
            if (jugador == 0) {
                heartsP1[vida_restante - 1].sprite = emptyLifeP1;
            }
            else if (jugador == 1) {
                heartsP2[vida_restante - 1].sprite = emptyLifeP2;
            }
            else if (jugador == 2) {
                heartsP3[vida_restante - 1].sprite = emptyLifeP3;
            }
            else if (jugador == 3) {
                heartsP4[vida_restante - 1].sprite = emptyLifeP4;
            }
        }
    }
    void UpdateUI() {
        for(int i = 0; i < jugadores.Count; ++i) {
            int health;
            // P1
            if(i == 0) {
                health = jugadores[0].GetComponent<PlayerController>().health;
                for(int j = 0; j < health; ++j) {
                    if(j < health) {
                        Debug.Log("SE EJECUTA");
                        heartsP1[j].sprite = fullLifeP1;
                    }
                    else {
                        heartsP1[j].sprite = emptyLifeP1;
                    }
                }
            }

            // P2
            if(i == 1) {
                health = jugadores[1].GetComponent<PlayerController>().health;
                for(int j = 0; j < health; ++j) {
                    if(j < health) {
                        heartsP2[j].sprite = fullLifeP2;
                    }
                    else {
                        heartsP2[j].sprite = emptyLifeP2;
                    }
                }
            }

            // P3
            if(i == 2) {
                health = jugadores[2].GetComponent<PlayerController>().health;
                for(int j = 0; j < health; ++j) {
                    if(j < health) {
                        heartsP3[j].sprite = fullLifeP3;
                    }
                    else {
                        heartsP3[j].sprite = emptyLifeP3;
                    }
                }
            }

            // P4
            if(i == 3) {
                health = jugadores[3].GetComponent<PlayerController>().health;
                for(int j = 0; j < health; ++j) {
                    if(j < health) {
                        heartsP4[j].sprite = fullLifeP4;
                    }
                    else {
                        heartsP4[j].sprite = emptyLifeP4;
                    }
                }
            }
        }
    }
}
