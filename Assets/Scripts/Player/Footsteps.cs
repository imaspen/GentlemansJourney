using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] footstepClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private AudioClip GetRandomFootstep()
    {
        return footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
    }

    public void FootStep()
    {
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        AudioClip clip = GetRandomFootstep();
        audioSource.PlayOneShot(clip);
    }
}
