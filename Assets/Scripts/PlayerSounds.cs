using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] hitClips;

    [SerializeField]
    private AudioClip[] hurtClips;

    [SerializeField]
    private AudioClip[] meleeContactClips;

    [SerializeField]
    private AudioClip[] footstepClips;

    [SerializeField]
    private AudioClip swingClip;

    // globally playing audio source
    private AudioSource audioSourceDamage;
    private AudioSource audioSourceMelee;
    private AudioSource audioSourceSteps;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceSteps = GameObject.Find("audioSourceSteps").GetComponent<AudioSource>();
        audioSourceMelee = GameObject.Find("audioSourceMelee").GetComponent<AudioSource>();
        audioSourceDamage = GameObject.Find("audioSourceDamage").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwingClip()
    {
        AudioClip clip = swingClip;
        audioSourceMelee.PlayOneShot(clip);
    }

    public void HurtClip()
    {
        AudioClip clip = GetRandomHurtClip();
        audioSourceDamage.PlayOneShot(clip);
    }

    private AudioClip GetRandomHurtClip()
    {
        return hurtClips[UnityEngine.Random.Range(0, hurtClips.Length)];

    }

    public void HitClip()
    {
        AudioClip clip = GetRandomHitClip();
        audioSourceDamage.PlayOneShot(clip);
    }

    private AudioClip GetRandomHitClip()
    {
        return hitClips[UnityEngine.Random.Range(0, hitClips.Length)];
    }

    public void MeleeContactClip()
    {
        AudioClip clip = GetRandomMeleeContactClip();
        audioSourceMelee.PlayOneShot(clip);
    }

    private AudioClip GetRandomMeleeContactClip()
    {
        return meleeContactClips[UnityEngine.Random.Range(0, meleeContactClips.Length)];
    }

    public void Footstep()
    {
        AudioClip clip = GetRandomFootstepClip();
    }

    private AudioClip GetRandomFootstepClip()
    {
        return footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
    }
}
