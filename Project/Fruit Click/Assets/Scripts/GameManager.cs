using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string PAUSE_LABEL = "Pause";
    private const string PLAY_LABEL = "Play";
    private const string REPLAY_LABEL = "Replay";
    private const string SCORE_FORMAT = "Score: {0}";

    private const float BOMB_SPAWN_CHANCE = 0.2f;
    private const float SPAWN_MARGIN = 10f;
    private const float FALLBACK_SIZE = 100f;
    private const int START_SCENE_INDEX = 0;
    private const float PLAY_SCALE_TIME = 1f;
    private const float STOP_SCALE_TIME = 0f;

    [Header("Game Settings")]
    [SerializeField]
    private float gameTime = 60f;

    [Header("References")]
    [SerializeField]
    private Canvas canvas;
    
    [SerializeField]
    private GameObject fruitPrefab;
    
    [SerializeField]
    private GameObject bombPrefab;

    [Header("Fruits")]
    [SerializeField]
    private FruitData[] fruitDataArray;

    [Header("Spawning")]
    [SerializeField]
    private float spawnIntervalStart = 2f;
    
    [SerializeField]
    private float spawnIntervalMin = 0.5f;
    
    [SerializeField]
    private float spawnIntervalDecrease = 0.1f;
    
    [SerializeField]
    private int maxSimultaneousFruits = 6;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    [SerializeField]
    private TextMeshProUGUI timerText;
    
    [SerializeField]
    private TextMeshProUGUI pauseButtonText;

    private int score;
    private bool isPaused;
    private bool gameOver;
    private float spawnTimer;
    private float currentSpawnInterval;
    private float currentTime;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentSpawnInterval = spawnIntervalStart;
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(GameLoop());
    }

    private void OnDestroy()
    {
        Time.timeScale = PLAY_SCALE_TIME;
    }

    public void TogglePause()
    {
        if (gameOver)
        {
            Time.timeScale = PLAY_SCALE_TIME;
            SceneManager.LoadSceneAsync(START_SCENE_INDEX);
            return;
        }

        isPaused = !isPaused;
        Time.timeScale = isPaused ? STOP_SCALE_TIME : PLAY_SCALE_TIME;
        pauseButtonText.text = isPaused ? PLAY_LABEL : PAUSE_LABEL;
    }

    private void SpawnItem()
    {
        if (Random.value < BOMB_SPAWN_CHANCE)
        {
            SpawnUIObject(bombPrefab, GetSafeSpawnPosition(bombPrefab));
        }
        else
        {
            FruitData chosen = GetWeightedRandomFruit();
            GameObject fruitObj = SpawnUIObject(fruitPrefab, GetSafeSpawnPosition(fruitPrefab));
            fruitObj.GetComponent<Fruit>().Initialize(chosen);
        }
    }

    private GameObject SpawnUIObject(GameObject prefab, Vector2 anchoredPos)
    {
        GameObject gameObj = Instantiate(prefab);
        gameObj.transform.SetParent(canvas.transform, false);
        gameObj.transform.localScale = Vector3.one;
        gameObj.transform.localRotation = Quaternion.identity;

        if (gameObj.TryGetComponent(out RectTransform rectTransform))
        {
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = anchoredPos;
        }
        else
        {
            gameObj.transform.localPosition = anchoredPos;
        }

        return gameObj;
    }

    private Vector2 GetSafeSpawnPosition(GameObject prefab)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        float canvasWidth  = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        Vector2 halfSize = GetPrefabHalfSize(prefab);

        float minX = -canvasWidth  / 2f + halfSize.x + SPAWN_MARGIN;
        float maxX =  canvasWidth  / 2f - halfSize.x - SPAWN_MARGIN;
        float minY = -canvasHeight / 2f + halfSize.y + SPAWN_MARGIN;
        float maxY =  canvasHeight / 2f - halfSize.y - SPAWN_MARGIN;

        if (minX > maxX)
        {
            minX = maxX = 0f;
        }

        if (minY > maxY)
        {
            minY = maxY = 0f;
        }

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private Vector2 GetPrefabHalfSize(GameObject prefab)
    {
        GameObject temp = Instantiate(prefab);

        temp.transform.SetParent(canvas.transform, false);
        temp.SetActive(false);

        Vector2 size = new Vector2(FALLBACK_SIZE, FALLBACK_SIZE);

        if (temp.TryGetComponent(out RectTransform rectTransform))
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);

            if (size.x <= 0f || size.y <= 0f)
            {
                size = rectTransform.sizeDelta;
            }
        }

        Destroy(temp);

        if (size.x <= 0f)
        {
            size.x = FALLBACK_SIZE;
        }

        if (size.y <= 0f)
        {
            size.y = FALLBACK_SIZE;
        }

        return size * 0.5f;
    }

    private FruitData GetWeightedRandomFruit()
    {
        float totalWeight = 0f;
        foreach (FruitData f in fruitDataArray) totalWeight += f.weight;

        float rand   = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (FruitData fruitData in fruitDataArray)
        {
            cumulative += fruitData.weight;

            if (rand <= cumulative)
            {
                return fruitData;
            }
        }

        return fruitDataArray[0];
    }

    /// <summary>
    /// Returns how many live (active) clickable items currently exist on the canvas.
    /// Using a live count avoids the counter leak that occurred when fruits/bombs
    /// destroyed themselves without being clicked.
    /// </summary>
    private int GetActiveItemCount()
    {
        if (canvas == null)
        {
            return 0;
        }

        return canvas.GetComponentsInChildren<ClickableBehaviour>(false).Length;
    }

    public void OnClicked(ClickableBehaviour clickableBehaviour, int points)
    {
        if (isPaused || gameOver)
        {
            return;
        }

        AddScore(points);
        clickableBehaviour.Destroy();
    }

    private void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText)
        {
            scoreText.SetText(SCORE_FORMAT, score);
        }
        if (timerText)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
    }

    private IEnumerator GameLoop()
    {
        currentTime = gameTime;

        while (currentTime > 0f && !gameOver)
        {
            if (!isPaused)
            {
                currentTime -= Time.deltaTime;

                currentSpawnInterval = Mathf.Max(
                    spawnIntervalMin,
                    spawnIntervalStart - (gameTime - currentTime) * spawnIntervalDecrease);

                UpdateUI();

                spawnTimer += Time.deltaTime;

                if (spawnTimer >= currentSpawnInterval)
                {
                    if (GetActiveItemCount() < maxSimultaneousFruits)
                    {
                        spawnTimer -= currentSpawnInterval;
                        SpawnItem();
                    }
                    else
                    {
                        // At the cap: hold the timer so we don't burst-spawn
                        // the moment a slot frees up.
                        spawnTimer = currentSpawnInterval;
                    }
                }
            }

            yield return null;
        }

        if (currentTime <= 0f)
        {
            gameOver = true;
            pauseButtonText.text = REPLAY_LABEL;
        }
    }
}