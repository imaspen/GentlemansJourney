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

    private Transform _enemies;

    private bool _moveFlag;

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
        _enemies = transform.Find("Enemies");
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        CharacterController = Player.GetComponent<CharacterController>();
    }

    public void InitDoors(GameObject frontRoom, GameObject backRoom, 
        GameObject leftRoom, GameObject rightRoom)
    {
        Debug.Log(_frontDoor);
        if (frontRoom == null) _frontDoor.SetActive(false);
        if (backRoom == null) _backDoor.SetActive(false);         
        if (leftRoom == null) _leftDoor.SetActive(false);         
        if (rightRoom == null) _rightDoor.SetActive(false);
        _frontRoom = frontRoom;
        _backRoom = backRoom;
        _leftRoom = leftRoom;
        _rightRoom = rightRoom;
    }

    private void Update()
    {
        _moveFlag = (Input.GetAxis("Use") > 0.5 
            && PlayerMovement.RoomMoveCooldown <= 0 
            && RoomEmpty());
    }

    private void FixedUpdate()
    {
        if (_moveFlag)
        {
            if (CheckDoor(_frontDoor)) MoveRoom(_frontRoom, 0, -1);
            if (CheckDoor(_backDoor)) MoveRoom(_backRoom, 0, 1);
            if (CheckDoor(_leftDoor)) MoveRoom(_leftRoom, -1, 0);
            if (CheckDoor(_rightDoor)) MoveRoom(_rightRoom, 1, 0);
        }
        _moveFlag = false;
    }

    private bool RoomEmpty()
    {
        return _enemies.childCount == 0;
    }

    private void MoveRoom(GameObject newRoom, float xOffset, float zOffset)
    {
        PlayerMovement.RoomMoveCooldown = 0.5f;
        Player.transform.position += new Vector3(xOffset, 0, zOffset);
        StartCoroutine(
            Camera.main.GetComponent<CameraMoveController>()
            .Lerp(gameObject, newRoom)
        );
    }

    private bool CheckDoor(GameObject door)
    {
        return CharacterController.bounds.Intersects(
            door.GetComponent<BoxCollider>().bounds
        ) && door.activeInHierarchy;
    }
}
