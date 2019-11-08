using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapDoor : MonoBehaviour
{
    private Collider _player;
    private Collider _collider;
    private float _enterTime;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Collider>();
        _collider = GetComponent<Collider>();
        _enterTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Use") > 0.5 
            && Time.time - _enterTime > 0.5
            && _player.bounds.Intersects(_collider.bounds))
        {
            SceneManager.LoadScene(2);
        }
    }
}
