using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] laughClips;

    [SerializeField]
    private AudioClip spitClip;

    [SerializeField]
    private AudioClip deathClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathClip()
    {
        AudioClip clip = deathClip;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void SpitClip()
    {
        AudioClip clip = spitClip;
        audioSource.PlayOneShot(clip);
    }

    public void LaughClip()
    {
        AudioClip clip = GetRandomLaughClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomLaughClip()
    {
        return laughClips[UnityEngine.Random.Range(0, laughClips.Length)];
    }
}
