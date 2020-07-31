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

    private GameObject[] prefabs = new GameObject[4];
    private int next_prefab = 1;

    private int next_player_joined = 1;
    public PlayerInputManager gestor;
    void Start()
    {
        gestor = GetComponent<PlayerInputManager>();
        gestor.playerPrefab = player_prefab_1;

        prefabs[0] = player_prefab_1;
        prefabs[1] = player_prefab_2;
        prefabs[2] = player_prefab_3;
        prefabs[3] = player_prefab_4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPlayerJoined() {
        Debug.Log("Player joined");
        gestor.playerPrefab = prefabs[next_prefab];
        next_prefab = (next_prefab+1)%4;
        Debug.Log("Next iter" + next_prefab);
    }
}
