using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string PAUSE_LABEL = "Pause";
    private const string PLAY_LABEL = "Play";
    private const string REPLAY_LABEL = "Replay";

    public static GameManager Instance { get; private set; }

    [Header("References")]
    public Canvas canvas;
    public GameObject fruitPrefab;
    public GameObject bombPrefab;

    [Header("Fruits")]
    public FruitData[] fruitDatas;

    [Header("Spawning")]
    public float spawnIntervalStart = 2f;
    public float spawnIntervalMin = 0.5f;
    public float spawnIntervalDecrease = 0.1f;
    public int maxSimultaneousFruits = 6;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pauseButtonText;

    private int score = 0;
    private float gameTime = 60f;
    private bool isPaused = false;
    private bool gameOver = false;
    private float spawnTimer = 0f;
    private float currentSpawnInterval;
    private int fruitsActive = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentSpawnInterval = spawnIntervalStart;

        // Fallback: auto-find a canvas if not assigned
        if (canvas == null)
            canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (gameTime > 0 && !gameOver)
        {
            if (!isPaused)
            {
                gameTime -= Time.deltaTime;

                // Ramp up spawn rate over time
                currentSpawnInterval = Mathf.Max(spawnIntervalMin,
                    spawnIntervalStart - (60f - gameTime) * spawnIntervalDecrease);

                UpdateUI();

                spawnTimer += Time.deltaTime;
                if (spawnTimer >= currentSpawnInterval)
                {
                    // Safety: recompute from the actual children count
                    int activeItems = CountActiveItems();

                    if (activeItems < maxSimultaneousFruits)
                    {
                        spawnTimer -= currentSpawnInterval; // subtract instead of reset
                        SpawnItem();
                    }
                    // else: don't reset spawnTimer, so we spawn as soon as a slot frees
                }
            }
            yield return null;
        }

        if (gameTime <= 0)
            pauseButtonText.text = REPLAY_LABEL;
    }

    /// <summary>
    /// Counts the fruits/bombs currently parented to the canvas.
    /// This is a safety net so the counter can never get stuck.
    /// </summary>
    private int CountActiveItems()
    {
        if (canvas == null) return fruitsActive;

        int count = 0;
        foreach (Transform child in canvas.transform)
        {
            if (child.GetComponent<Fruit>() != null || child.GetComponent<Bomb>() != null)
                count++;
        }

        // Sync the counter to reality (prevents drift)
        fruitsActive = count;
        return count;
    }

        private void SpawnItem()
    {
        bool isBomb = Random.value < 0.2f;

        if (isBomb)
        {
            Vector2 pos = GetSafeSpawnPosition(bombPrefab);
            GameObject bomb = SpawnUIObject(bombPrefab, pos);
            fruitsActive++;
        }
        else
        {
            FruitData chosen = GetWeightedRandomFruit();
            Vector2 pos = GetSafeSpawnPosition(fruitPrefab);
            GameObject fruitObj = SpawnUIObject(fruitPrefab, pos);
            Fruit fruit = fruitObj.GetComponent<Fruit>();
            fruit.Initialize(chosen);
            fruitsActive++;
        }
    }

    /// <summary>
    /// Instantiates a UI prefab as a child of the canvas,
    /// and correctly places it using anchoredPosition.
    /// </summary>
    private GameObject SpawnUIObject(GameObject prefab, Vector2 anchoredPos)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(canvas.transform, false); // false = keep local transform, not world
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;

        RectTransform rt = obj.GetComponent<RectTransform>();
        if (rt != null)
        {
            // Force center anchor/pivot so anchoredPosition (0,0) = center of canvas
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = anchoredPos;
        }
        else
        {
            // Fallback for non-UI prefabs
            obj.transform.localPosition = anchoredPos;
        }

        return obj;
    }

    /// <summary>
    /// Returns a random anchoredPosition inside the Canvas,
    /// keeping the prefab's full size on screen.
    /// </summary>
    private Vector2 GetSafeSpawnPosition(GameObject prefab)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        Vector2 halfSize = GetPrefabHalfSize(prefab);
        float margin = 10f;

        float minX = -canvasWidth / 2f + halfSize.x + margin;
        float maxX = canvasWidth / 2f - halfSize.x - margin;
        float minY = -canvasHeight / 2f + halfSize.y + margin;
        float maxY = canvasHeight / 2f - halfSize.y - margin;

        if (minX > maxX) { minX = maxX = 0f; }
        if (minY > maxY) { minY = maxY = 0f; }

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    /// <summary>
    /// Returns half the size of the prefab's RectTransform (in canvas pixels).
    /// </summary>
    private Vector2 GetPrefabHalfSize(GameObject prefab)
    {
        // Instantiate temporarily as a child of the canvas so its RectTransform is valid
        GameObject temp = Instantiate(prefab);
        temp.transform.SetParent(canvas.transform, false);
        temp.SetActive(false);

        Vector2 size = new Vector2(100f, 100f); // default fallback

        RectTransform rt = temp.GetComponent<RectTransform>();
        if (rt != null)
        {
            // LayoutRebuilder ensures size is up to date if using layout components
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
            size = new Vector2(rt.rect.width, rt.rect.height);

            // If size is still 0, try sizeDelta
            if (size.x <= 0f || size.y <= 0f)
                size = rt.sizeDelta;
        }

        Destroy(temp);

        if (size.x <= 0f) size.x = 100f;
        if (size.y <= 0f) size.y = 100f;

        return size * 0.5f;
    }

    private FruitData GetWeightedRandomFruit()
    {
        float totalWeight = 0f;
        foreach (var f in fruitDatas) totalWeight += f.weight;
        float rand = Random.Range(0f, totalWeight);
        float cumulative = 0f;
        foreach (var f in fruitDatas)
        {
            cumulative += f.weight;
            if (rand <= cumulative) return f;
        }
        return fruitDatas[0];
    }


    public void OnFruitClicked(Fruit fruit, float clickProgress, Vector3 worldPos)
    {
        if (isPaused || gameOver) return;

        FruitData data = fruit.Data;

        int points = 0;

        points = data.basePoints;
        
        AddScore(points);
        fruit.DestroyFruit();
        fruitsActive--;
    }

    public void OnBombClicked(Bomb bomb, Vector3 worldPos)
    {
        if (isPaused || gameOver) return;
        int penalty = -10;
        AddScore(penalty);
        bomb.DestroyBomb();
        fruitsActive--;
    }

    private void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (timerText) timerText.text = Mathf.CeilToInt(gameTime).ToString();
    }

    public void TogglePause()
    {
        if (pauseButtonText.text.Equals(REPLAY_LABEL))
        {
            SceneManager.LoadSceneAsync(0);
            return;
        }

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseButtonText.text = isPaused ? PLAY_LABEL : PAUSE_LABEL;
    }

    public void PauseButton() => TogglePause();

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}