using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovementController : MonoBehaviour
{
    private GameObject _frontDoor;
    private GameObject _backDoor;
    private GameObject _leftDoor;
    private GameObject _rightDoor;

    private GameObject _frontRoom;
    private GameObject _backRoom;
    private GameObject _leftRoom;
    private GameObject _rightRoom;

    private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set { _player = value; }
    }
    
    private CharacterController _characterController;
    public CharacterController CharacterController
    {
        get { return _characterController; }
        set { _characterController = value; }
    }

    private PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement
    {
        get { return _playerMovement; }
        set { _playerMovement = value; }
    }

    private void Awake()
    {
        _frontDoor = transform.Find("Meshes/Front Door").gameObject;
        _backDoor = transform.Find("Meshes/Back Door").gameObject;
        _leftDoor = transform.Find("Meshes/Left Door").gameObject;
        _rightDoor = transform.Find("Meshes/Right Door").gameObject;
    }
    
    public void InitDoors(GameObject frontRoom, GameObject backRoom, 
        GameObject leftRoom, GameObject rightRoom)
    {
        if (!frontRoom) _frontDoor.SetActive(false);
        if (!backRoom) _backDoor.SetActive(false);         
        if (!leftRoom) _leftDoor.SetActive(false);         
        if (!rightRoom) _rightDoor.SetActive(false);
        _frontRoom = frontRoom;
        _backRoom = backRoom;
        _leftRoom = leftRoom;
        _rightRoom = rightRoom;
        Player = GameObject.FindGameObjectWithTag("Player");
        CharacterController = Player.GetComponent<CharacterController>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (PlayerMovement.RoomMoveCooldown <= 0 && Input.GetAxis("AttackMelee") > 0.2)
        {
            if (CheckDoor(_frontDoor)) MoveRoom(_frontRoom, 0, -1);
            if (CheckDoor(_backDoor)) MoveRoom(_backRoom, 0, 1);
            if (CheckDoor(_leftDoor)) MoveRoom(_leftRoom, -1, 0);
            if (CheckDoor(_rightDoor)) MoveRoom(_rightRoom, 1, 0);
        }
    }

    private void MoveRoom(GameObject newRoom, float xOffset, float zOffset)
    {
        PlayerMovement.RoomMoveCooldown = 0.5f;
        newRoom.SetActive(true);
        Player.transform.position = new Vector3(
            Player.transform.position.x + xOffset,
            Player.transform.position.y,
            Player.transform.position.z + zOffset
        );
        Transform cameraPoint = newRoom.transform.Find("Camera Point");
        Camera.main.transform.position = cameraPoint.position;
        gameObject.SetActive(false);
    }

    private bool CheckDoor(GameObject door)
    {
        return CharacterController.bounds.Intersects(
            door.GetComponents<BoxCollider>()[1].bounds
        );
    }
}
