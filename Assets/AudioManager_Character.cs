using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Character : MonoBehaviour
{
    public AudioSource Shoots;
    public AudioSource Jump;
    public AudioSource Reflect;
    public AudioSource Pick_ammo;
    public AudioSource Dash;



    // Start is called before the first frame update
    void Awake()
    {
        Shoots = gameObject.AddComponent<AudioSource>();
        Jump = gameObject.AddComponent<AudioSource>();
        Reflect = gameObject.AddComponent<AudioSource>();
        Pick_ammo = gameObject.AddComponent<AudioSource>();
        Dash = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
