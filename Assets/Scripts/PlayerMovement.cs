using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public float playerSpeed;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _lookDirection = Vector3.zero;
    private float _cameraAngle;

    private float _roomMoveCooldown = 0.0f;
    public float RoomMoveCooldown
    {
        get { return _roomMoveCooldown; }
        set { _roomMoveCooldown = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //_cameraAngle = Camera.main.transform.rotation.y * Mathf.Rad2Deg;
        _cameraAngle = -15;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        if (_moveDirection.magnitude > 0.95f)
        {
            _moveDirection.Normalize();
            _moveDirection *= playerSpeed;

            characterController.Move(_moveDirection);
        }


    }
    void Update()
    {
        RoomMoveCooldown -= Time.deltaTime;
        Vector3 lookDirection = new Vector3(Input.GetAxis("LookHorizontal"), 0, Input.GetAxis("LookVertical"));

        if (lookDirection.magnitude > 0.2f)
        {
            var angle = Mathf.Atan2(Input.GetAxis("LookHorizontal"), -Input.GetAxis("LookVertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle + _cameraAngle, 0);

        }
    }
}
