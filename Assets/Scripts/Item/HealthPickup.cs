using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    HealthTracker playerHealth;

    private bool itemPickedUp;

    [SerializeField]
    private float _healthBonus;

    [SerializeField]
    private AudioClip pickupClip;

    private AudioSource audioSource;

    private Renderer rend;

    public float HealthBonus
    {
        get { return _healthBonus; }
        set { _healthBonus = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        itemPickedUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemPickedUp == false)
        {
            if (other.tag == "Player")
            {
                itemPickedUp = true;
                audioSource.PlayOneShot(pickupClip);
                Debug.Log(playerHealth.CurrentHealth);
                playerHealth.AddHealth(HealthBonus);
                Debug.Log(playerHealth.CurrentHealth);
                StartCoroutine(DestroySequence());
            }
        }
    }

    IEnumerator DestroySequence()
    {
        rend.enabled = false;
        gameObject.GetComponentInChildren<Light>().enabled = false;
        yield return new WaitForSeconds(1f);
        itemPickedUp = false;
        Destroy(gameObject);
    }
}
