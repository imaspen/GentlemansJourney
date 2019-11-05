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

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            transform.position += _movementPerSecond * Time.deltaTime;
        }
    }

    public IEnumerator Lerp(GameObject startRoom, GameObject endRoom)
    {
        endRoom.SetActive(true);
        _target = endRoom.transform.Find("Camera Point");
        _movementPerSecond = (_target.position - transform.position);
        yield return new WaitForSeconds(1.0f);
        startRoom.SetActive(false);
        endRoom.transform.Find("Enemies").gameObject.SetActive(true);
        _target = null;
    }
}
