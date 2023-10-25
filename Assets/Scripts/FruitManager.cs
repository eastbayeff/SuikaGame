using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance;

    [SerializeField] private CinemachineVirtualCamera cam;
    [Header("Fruit")]
    [SerializeField] private List<Sprite> _fruitSprites;

    [SerializeField] private Fruit _fruitPrefab;

    [SerializeField] private float _fruitSizeMultiplier = 0.25f;
    public float FruitSizeMultiplier => _fruitSizeMultiplier;

    [SerializeField] private SpriteRenderer _nextFruitSpriteRenderer;
    private int _nextFruitSize;

    [Header("Sound Effects")]
    [SerializeField] private List<AudioClip> _fruitSounds;
    [SerializeField] private List<AudioClip> _spawnSounds;

    [Header("Scoring")]
    [SerializeField] private TextMesh _scoreText;
    [SerializeField] private UpgradeFruitWheel _fruitWheel;
    private int _highestCombo = 0;

    [Header("Events")]
    public UnityEvent OnGameOver;

    public bool IsGameOver => _gameOver;
    private bool _gameOver = false;

    private void Awake() => Instance = this;
    private void Start() => DisplayNextFruit();
    
    public Sprite GetFruitSprite(int size) => _fruitSprites[size];

    public void CreateNewFruit(int size, Vector3 position)
    {
        _scoreText.text = (int.Parse(_scoreText.text) + (size * size * 3)).ToString();

        // TODO: handle final fruit combination
        if (size == _fruitSprites.Count) return;

        if (size > _highestCombo)
        {
            _highestCombo = size;
            _fruitWheel.Unlock(size);
        }

        var newFruit = Instantiate(_fruitPrefab, position, Quaternion.identity);
        newFruit.Size = size;
    }

    public void CreateNewFruit(Vector3 position)
    {
        var newFruit = Instantiate(_fruitPrefab, position, Quaternion.identity);
        newFruit.Size = _nextFruitSize;
        DisplayNextFruit();
    }

    private void DisplayNextFruit()
    {
        _nextFruitSize = Random.Range(0, 3);
        _nextFruitSpriteRenderer.sprite = _fruitSprites[_nextFruitSize];
    }

    public void GameOver(Transform gameOverFruit)
    {
        OnGameOver.Invoke();
        _gameOver = true;

        cam.Follow = gameOverFruit;
        cam.LookAt = gameOverFruit;

        // change the orthographic size over time to become zoomed out
        StartCoroutine(ZoomOut());

        IEnumerator ZoomOut()
        {
            var t = 0f;
            var startSize = cam.m_Lens.OrthographicSize;
            var endSize = 300f;
            while (t < 3.5f)
            {
                t += Time.unscaledDeltaTime;

                float progress = t / 5f;
                cam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, progress);

                yield return null;
            }

            SceneManager.LoadScene("GameOver");
        }
    }

    public void PlayFruitSFX(int size)
    {
        GetComponent<AudioSource>().PlayOneShot(_fruitSounds[size]);
    }

    public void PlaySpawnSFX()
    {
        GetComponent<AudioSource>().PlayOneShot(_spawnSounds[Random.Range(0, _spawnSounds.Count)]);
    }

}