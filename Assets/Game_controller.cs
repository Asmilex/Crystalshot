using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    List<GameObject> jugadores = new List<GameObject>();

    // UI
    public int health = 3;
    public int maxHealth = 3;

    public Image[] hearts;
    public Sprite fullLife;
    public Sprite emptyLife;

    void Start()
    {
        Screen.SetResolution(2048,1024, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth) {
            health = maxHealth;
        }

        for(int i = 0; i < hearts.Length; ++i) {
            hearts[i].sprite = (i < health) ? fullLife : emptyLife;
            hearts[i].enabled = (i < maxHealth) ? true : false; 
        }
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

    public void UpdateUI() {
        health = jugadores[0].GetComponent<PlayerController>().health;
    }
}
