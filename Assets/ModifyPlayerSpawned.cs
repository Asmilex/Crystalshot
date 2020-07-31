using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ModifyPlayerSpawned : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player_prefab_1;
    public GameObject player_prefab_2;
    public GameObject player_prefab_3;
    public GameObject player_prefab_4;

    private int next_player_joined = 1;
    public PlayerInputManager gestor;
    void Start()
    {
        gestor = GetComponent<PlayerInputManager>();
        gestor.playerPrefab = player_prefab_1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPlayerJoined() {
        if (next_player_joined == 1) {
            next_player_joined++;
            gestor.playerPrefab = player_prefab_2;
        }
        if (next_player_joined == 2) {
            next_player_joined++;
            gestor.playerPrefab = player_prefab_3;
        }
        if (next_player_joined == 3) {
            next_player_joined++;
            gestor.playerPrefab = player_prefab_4;
        }
    }
}
