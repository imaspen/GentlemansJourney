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

    private GameObject[,] _layout;
    private int _midpointX;
    private int _midpointZ;
    private Dictionary<int, Vector3> _offsets;
    private Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _layout = new GameObject[LevelWidth, LevelHeight];
        _midpointX = (LevelWidth - 1) / 2;
        _midpointZ = (LevelWidth - 1) / 2;
        _layout[_midpointX, _midpointZ] = StartRoom;

        generateLayout();
        spawnRooms();
    }

    //Dirty Implementation
    private void generateLayout()
    {
        for (int i = 0; i < RoomCount; i++)
        {
            int j = 0;
            while (true)
            {
                int posX = Random.Range(0, _levelHeight);
                int posY = Random.Range(0, _levelWidth);
                if (_layout[posX, posY]) continue;

                if ((posX > 0 && _layout[posX - 1, posY])
                    || (posY > 0 && _layout[posX, posY - 1])
                    || (posX < LevelWidth - 1 && _layout[posX + 1, posY])
                    || (posY < LevelHeight - 1 && _layout[posX, posY + 1]))
                {
                    _layout[posX, posY] = Rooms[0];
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
    }

    private void spawnRooms()
    {
        for (int z = 0; z < LevelHeight - 1; z++)
        {
            float worldZ = (z - _midpointZ) * 3; 
            for (int x = 0; x < LevelWidth - 1; x++)
            {
                if (_layout[x, z]) {
                    float worldX = (x - _midpointX) * 5;

                    Vector3 position = new Vector3(worldX, 0, worldZ);
                    GameObject room = z == _midpointZ && x == _midpointX 
                        ? StartRoom 
                        : Instantiate(Rooms[0], position, Quaternion.identity);

                    room.GetComponent<RoomMovementController>().InitDoors(
                        z > 0 ? _layout[x, z - 1] : null,
                        z < LevelHeight - 1 ? _layout[x, z + 1] : null,
                        x > 0 ? _layout[x - 1, z] : null,
                        x < LevelWidth - 1 ? _layout[x + 1, z] : null
                    );

                    room.SetActive(false);
                }
            }
        }
        _layout[_midpointX, _midpointZ].SetActive(true);
    }
}
