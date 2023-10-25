using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;
    [SerializeField] private Transform _holdTransform;
    [SerializeField] private float _spawnCooldown = 0.2f;
    private float _spawnTimer = 0f;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (FruitManager.Instance.IsGameOver) return;   

        // grab intended move direction from player mouse input
        var newPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);

        // clamp transform X to edges
        newPosition.x = Mathf.Clamp(newPosition.x, _leftEdge.position.x, _rightEdge.position.x);

        // move transform toward X of mouse position
        transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
    }

    private void Update()
    {
        if (FruitManager.Instance.IsGameOver) return;

        _spawnTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _spawnTimer >= _spawnCooldown)
        {
            _spawnTimer = 0f;
            FruitManager.Instance.PlaySpawnSFX();
            FruitManager.Instance.CreateNewFruit(_holdTransform.position);
        }
    }
}
