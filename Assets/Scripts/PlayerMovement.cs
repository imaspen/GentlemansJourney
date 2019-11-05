using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public float playerSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        if (moveDirection.magnitude > 0.95f)
        {
            moveDirection.Normalize();
            moveDirection *= playerSpeed;

            characterController.Move(moveDirection);
        }


    }
    void Update()
    {
        RoomMoveCooldown -= Time.deltaTime;
        Vector3 lookDirection = new Vector3(Input.GetAxis("LookHorizontal"), 0, Input.GetAxis("LookVertical"));

        //Debug.Log($"X: {lookDirection.x}, Z: {lookDirection.z}");

        if (lookDirection.magnitude > 0.2f)
        {
            //transform.rotation = Quaternion.LookRotation(lookDirection);
            var angle = Mathf.Atan2(Input.GetAxis("LookHorizontal"), -Input.GetAxis("LookVertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);

        }
    }
}
