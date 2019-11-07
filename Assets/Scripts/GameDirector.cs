using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject deathScreen;
    private bool deathActive;

    [SerializeField]
    private AudioClip[] deathClips;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip deathStinger;

    // Start is called before the first frame update
    void Start()
        
    {
        audioSource = GetComponent<AudioSource>();
        deathActive = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            if (!deathActive)
            {
                audioSource.PlayOneShot(deathStinger);
                DeathQuote();
                deathActive = true;
                Instantiate(deathScreen);
            }
        }
    }

    private AudioClip GetRandomDeathQuote()
    {
        return deathClips[UnityEngine.Random.Range(0, deathClips.Length)];
    }

    void DeathQuote()
    {
        AudioClip clip = GetRandomDeathQuote();
        audioSource.PlayOneShot(clip);
    }
}
