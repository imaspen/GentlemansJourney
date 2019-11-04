using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovementController : MonoBehaviour
{
    private GameObject _frontDoor;
    private GameObject _backDoor;
    private GameObject _leftDoor;
    private GameObject _rightDoor;
   
    private void Awake()
    {
        _frontDoor = transform.Find("Meshes/Front Door").gameObject;
        _backDoor = transform.Find("Meshes/Back Door").gameObject;
        _leftDoor = transform.Find("Meshes/Left Door").gameObject;
        _rightDoor = transform.Find("Meshes/Right Door").gameObject;
        Debug.Log(_frontDoor);
    }
    
    public void InitDoors(GameObject frontRoom, GameObject backRoom, 
        GameObject leftRoom, GameObject rightRoom)
    {
        if (!frontRoom) _frontDoor.SetActive(false);
        if (!backRoom) _backDoor.SetActive(false);         
        if (!leftRoom) _leftDoor.SetActive(false);         
        if (!rightRoom) _rightDoor.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetAxis("AttackMelee") > 0.2)
        {

        }
    }
}
