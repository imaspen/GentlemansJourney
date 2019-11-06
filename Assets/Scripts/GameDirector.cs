using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            Instantiate(deathScreen);
        }
    }
}
