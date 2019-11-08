using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private GameObject player;
    private float _doorLockedCooldown;

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

    [SerializeField]
    private GameObject bossMusic;

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
        if (_doorLockedCooldown > 0) _doorLockedCooldown -= Time.deltaTime;
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
        if (_doorLockedCooldown <= 0)
        {
            audioSource.PlayOneShot(doorLockedButler);
            _doorLockedCooldown = 5.0f;
        }
    }

    public void SpawnBossMusic()
    {
        Instantiate(bossMusic);
    }
}
