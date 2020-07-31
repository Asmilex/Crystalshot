using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public GameObject sprite;
    public float time = 0.07f;
    private Sprite ghostSprite;
    private float colorAlpha;


    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        ghostSprite = sprite.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<PlayerController>().dashing) {
            if(ghostDelaySeconds > 0) {
                ghostDelaySeconds -= Time.deltaTime;
            }

            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            Sprite currentSprite = ghostSprite;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            ghostDelaySeconds = ghostDelay;
            
            Destroy(currentGhost, time);
        }
    }
}
