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

    [SerializeField]
    private AudioClip doorLockedButler;

    public HashSet<GameObject> CompletedRooms;

    // Start is called before the first frame update
    void Start()
        
    {
        CompletedRooms = new HashSet<GameObject>();
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
                //DeathQuote();
                deathActive = true;
            }
        }
    }

    private AudioClip GetRandomDeathQuote()
    {
        return deathClips[UnityEngine.Random.Range(0, deathClips.Length)];
    }

    public void DeathQuote()
    {
        Instantiate(deathScreen);
        audioSource.PlayOneShot(deathStinger);
        AudioClip clip = GetRandomDeathQuote();
        audioSource.PlayOneShot(clip);
    }

    public void DoorLockedQuote()
    {
        audioSource.PlayOneShot(doorLockedButler);
    }
}
