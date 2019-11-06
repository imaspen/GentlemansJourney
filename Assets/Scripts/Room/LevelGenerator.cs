using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _startRoom;
    public GameObject StartRoom
    {
        get { return _startRoom; }
        set { _startRoom = value; }
    }

    [SerializeField]
    private GameObject _endRoom;
    public GameObject EndRoom
    {
        get { return _endRoom; }
        set { _endRoom = value; }
    }

    [SerializeField]
    private int _roomCount = 8;
    public int RoomCount
    {
        get { return _roomCount; }
        set { if (value > 0) _roomCount = value; }
    }

    [SerializeField]
    private List<GameObject> _rooms;
    public List<GameObject> Rooms
    {
        get { return _rooms; }
        set { _rooms = value; }
    }

    [SerializeField]
    private int _levelWidth = 7;
    public int LevelWidth
    {
        get { return _levelWidth; }
        set { if (value % 2 == 1) _levelWidth = value; }
    }

    [SerializeField]
    private int _levelHeight = 7;
    public int LevelHeight
    {
        get { return _levelHeight; }
        set { if (value % 2 == 1) _levelHeight = value; }
    }

    [SerializeField]
    private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private int[,] _layout;
    private GameObject[,] _map;
    private int _midpointX;
    private int _midpointZ;

    void Awake()
    {
        Instantiate(Player, new Vector3(0, 0.4f, 0), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        _layout = new int[LevelWidth, LevelHeight];
        _midpointX = (LevelWidth - 1) / 2;
        _midpointZ = (LevelHeight - 1) / 2;
        _layout[_midpointX, _midpointZ] = 0;

        generateLayout();
        spawnRooms();
    }

    //Dirty Implementation
    private void generateLayout()
    {
        _layout[_midpointX, _midpointZ] = 1;
        for (int i = 0; i < RoomCount; i++)
        {
            int j = 0;
            while (true)
            {
                int posX = Random.Range(0, _levelHeight);
                int posY = Random.Range(0, _levelWidth);
                if (_layout[posX, posY] > 0) continue;

                if ((posX > 0 && _layout[posX - 1, posY] > 0)
                    || (posY > 0 && _layout[posX, posY - 1] > 0)
                    || (posX < LevelWidth - 1 && _layout[posX + 1, posY] > 0)
                    || (posY < LevelHeight - 1 && _layout[posX, posY + 1] > 0))
                {
                    _layout[posX, posY] = Random.Range(1, Rooms.Count + 1);
                    break;
                }
                if (j++ > 1000)
                {
                    Debug.Log("I think we're stuck chief");
                    Application.Quit();
                    break;
                }
            }
        }
        while (true)
        {
            int posX = Random.Range(0, _levelHeight);
            int posY = Random.Range(0, _levelWidth);
            if (_layout[posX, posY] > 0) continue;

            if ((posX > 0 && _layout[posX - 1, posY] > 0 ? 1 : 0)
                + (posY > 0 && _layout[posX, posY - 1] > 0 ? 1 : 0)
                + (posX < LevelWidth - 1 && _layout[posX + 1, posY] > 0 ? 1 : 0)
                + (posY < LevelHeight - 1 && _layout[posX, posY + 1] > 0 ? 1 : 0)
                == 1)
            {
                _layout[posX, posY] = Rooms.Count + 1;
                break;
            }
        }
    }

    private void spawnRooms()
    {
        _map = new GameObject[LevelWidth, LevelHeight];
        for (int z = 0; z < LevelHeight; z++)
        {
            float worldZ = (z - _midpointZ) * 3; 
            for (int x = 0; x < LevelWidth; x++)
            {
                if (_layout[x, z] > 0) {

                    float worldX = (x - _midpointX) * 5;

                    Vector3 position = new Vector3(worldX, 0, worldZ);
                    GameObject room = z == _midpointZ && x == _midpointX 
                        ? StartRoom 
                        : Instantiate(
                            (_layout[x, z] - 1 == Rooms.Count) 
                                ? _endRoom 
                                : Rooms[_layout[x, z] - 1], 
                            position, 
                            Quaternion.identity
                        );

                    _map[x, z] = room;

                    _map[x, z].SetActive(false);
                }
            }
        }
        _map[_midpointX, _midpointZ].SetActive(true);

        for (int z = 0; z < LevelHeight; z++)
        {
            for (int x = 0; x < LevelWidth; x++)
            {
                if (_map[x, z])
                {
                    _map[x, z].GetComponent<RoomMovementController>().InitDoors(
                        z > 0 ? _map[x, z - 1] : null,
                        z < LevelHeight - 1 ? _map[x, z + 1] : null,
                        x > 0 ? _map[x - 1, z] : null,
                        x < LevelWidth - 1 ? _map[x + 1, z] : null
                    );
                }
            }
        }
    }
}
