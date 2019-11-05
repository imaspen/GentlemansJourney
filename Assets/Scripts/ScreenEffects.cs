using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private bool _shakingFlag;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        _shakingFlag = false;
        shakeDuration = 0;
    }

    public void StartShake (float duration)
    {
        Debug.Log("START SHAKE");
        originalPos = camTransform.localPosition;
        _shakingFlag = true;
        shakeDuration = duration;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if (_shakingFlag)
        {
            _shakingFlag = false;
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
