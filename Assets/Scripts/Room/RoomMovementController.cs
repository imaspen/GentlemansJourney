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

    private GameDirector _gameDirector;
    private LevelGenerator _levelGenerator;

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
        _gameDirector = GameObject.Find("Game Director")
            .GetComponent<GameDirector>();
        _levelGenerator = GameObject.Find("Level Controller")
            .GetComponent<LevelGenerator>();
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
            if (CheckDoor(_frontDoor, _frontRoom)) MoveRoom(_frontRoom, 0, -1);
            if (CheckDoor(_backDoor, _backRoom)) MoveRoom(_backRoom, 0, 1);
            if (CheckDoor(_leftDoor, _leftRoom)) MoveRoom(_leftRoom, -1, 0);
            if (CheckDoor(_rightDoor, _rightRoom)) MoveRoom(_rightRoom, 1, 0);
        }
        _moveFlag = false;
    }

    private bool RoomEmpty()
    {
        return _enemies.childCount == 0;
    }

    private void MoveRoom(GameObject newRoom, float xOffset, float zOffset)
    {
        PlayerMovement.RoomMoveCooldown = 1.0f;
        Player.transform.position += new Vector3(xOffset, 0, zOffset);
        StartCoroutine(
            Camera.main.GetComponent<CameraMoveController>()
            .Lerp(gameObject, newRoom)
        );
    }

    private bool CheckDoor(GameObject door, GameObject room)
    {
        _gameDirector.CompletedRooms.Add(gameObject);
        Debug.Log(_gameDirector.CompletedRooms.Count);

        if (!CharacterController.bounds.Intersects(
            door.GetComponent<BoxCollider>().bounds)) return false;

        if (!door.activeInHierarchy) return false;

        if ((room.name != $"{_levelGenerator.EndRoom.name}(Clone)"
                || _gameDirector.CompletedRooms.Count
                    > _levelGenerator.RoomCount))
        {
            return true;
        }
        else
        {
            //play locked noise
            return false;
        }
    }
}
