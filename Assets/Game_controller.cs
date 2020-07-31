using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    List<GameObject> jugadores = new List<GameObject>();
    public GameObject canvas;
    Image[] imagenes_canvas;

    void Start()
    {
        Screen.SetResolution(2048,1024, true);

        imagenes_canvas =  canvas.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }

    public void add_player(GameObject player) {
        jugadores.Add(player);
    }

    void UpdateUI (char color, int vida_restante) {
        if (color == 'b') {
            if (vida_restante == 2) {
                Destroy(GameObject.Find("Image_blue3"));
            }
            if (vida_restante == 1) {
                Destroy(GameObject.Find("Image_blue2"));
            }
            if (vida_restante == 0) {
                Destroy(GameObject.Find("Image_blue1"));
            }
        }
        if (color == 'r') {
            if (vida_restante == 2) {
                Destroy(GameObject.Find("Image_red3"));
            }
            if (vida_restante == 1) {
                Destroy(GameObject.Find("Image_red2"));
            }
            if (vida_restante == 0) {
                Destroy(GameObject.Find("Image_red1"));
            }

        }
        if (color == 'y') {
            if (vida_restante == 2) {
                Destroy(GameObject.Find("Image_yellow3"));
            }
            if (vida_restante == 1) {
                Destroy(GameObject.Find("Image_yellow2"));
            }
            if (vida_restante == 0) {
                Destroy(GameObject.Find("Image_yellow1"));
            }
        }
        if (color == 'g') {
            if (vida_restante == 2) {
                Destroy(GameObject.Find("Image_green3"));
            }
            if (vida_restante == 1) {
                Destroy(GameObject.Find("Image_green2"));
            }
            if (vida_restante == 0) {
                Destroy(GameObject.Find("Image_green1"));
            }
        }

    }

    public void update_health(GameObject jugador) {
        for (int i = 0; i < jugadores.Count; i++) {
            if (jugadores[i].GetInstanceID() == jugador.GetInstanceID()) {

                UpdateUI(jugador.GetComponent<PlayerController>().color, jugador.GetComponent<PlayerController>().health);

                if (jugadores[i].GetComponent<PlayerController>().health == 0) {
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
