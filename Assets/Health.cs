using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;

    public Image[] hearts;
    public Sprite fullLife;
    public Sprite emptyLife;
 
    // Start is called before the first frame update
    void Start()
    {
        
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
}
