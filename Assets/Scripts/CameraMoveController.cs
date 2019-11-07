using System.Collections;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    
    private Transform _target;
    private Vector3 _movementPerSecond;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public IEnumerator Lerp(GameObject startRoom, GameObject endRoom)
    {
        endRoom.SetActive(true);
        _target = endRoom.transform.Find("Camera Point");
        _movementPerSecond = (_target.position - transform.position) / Speed;
        _rigidbody.velocity = _movementPerSecond;
        yield return new WaitForSeconds(Speed);
        _rigidbody.velocity = Vector3.zero;
        transform.position = _target.position;
        startRoom.SetActive(false);
        endRoom.transform.Find("Enemies").gameObject.SetActive(true);
        _target = null;
    }
}
