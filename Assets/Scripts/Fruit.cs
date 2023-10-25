using Cinemachine;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private int _size = 0;

    [HideInInspector]
    public bool HasCollided = false;

    public int Size 
    { 
        get { return _size; } 
        set 
        { 
            _size = value;
            transform.localScale = (1 + _size) * FruitManager.Instance.FruitSizeMultiplier * Vector3.one;
            GetComponent<SpriteRenderer>().sprite = FruitManager.Instance.GetFruitSprite(_size);
        } 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Fruit fruit) && fruit.Size == _size)
        {
            if (HasCollided)
            {
                Destroy(gameObject);
                return;
            }

            fruit.HasCollided = true;
            var spawnPos = (transform.position + other.transform.position) / 2;
            FruitManager.Instance.CreateNewFruit(_size + 1, spawnPos);
            FruitManager.Instance.PlayFruitSFX(_size);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        FruitManager.Instance.GameOver(other.transform);
    }
}