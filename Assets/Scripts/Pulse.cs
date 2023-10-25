using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _scale = 1f;

    private Vector3 startScale;
    private Vector3 targetScale;

    void Start()
    {
        transform.localScale = startScale;
        targetScale = startScale * _scale;
    }

    void Update()
    {
        var pingPong = Mathf.PingPong(Time.time * _speed, 1);
        transform.localScale = Vector3.Lerp(startScale, targetScale, pingPong);
    }
}
