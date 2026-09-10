using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    public Image fruitImage;
    public Image progressBar;

    [HideInInspector] public FruitData Data { get; private set; }

    private float elapsed = 0f;
    private bool isAlive = true;

    public void Initialize(FruitData data)
    {
        Data = data;
        fruitImage.sprite = data.sprite;
        progressBar.fillAmount = 1f;

        // Start timer
        elapsed = 0f;
        isAlive = true;
    }

    private void Update()
    {
        if (!isAlive || GameManager.Instance == null || Time.timeScale == 0) return;

        elapsed += Time.deltaTime;

        float progress = elapsed / Data.timeToLive;

        progressBar.fillAmount -= progress;

        // Check if time expired
        if (progressBar.fillAmount <= 0f)
        {
            // Fruit disappears without points
            DestroyFruit();
        }
    }

    private void OnMouseDown()
    {
        if (!isAlive) return;

        // Convert click position to local position relative to progress bar
        Vector2 localPoint;
        RectTransform barRect = progressBar.rectTransform;
        Camera cam = Camera.main;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(barRect, Input.mousePosition, cam, out localPoint);

        // Normalize local x to 0..1 (0 = left edge, 1 = right edge)
        float barWidth = barRect.rect.width;
        float normalizedX = (localPoint.x / barWidth) + 0.5f;
        normalizedX = Mathf.Clamp01(normalizedX);

        // Notify GameManager
        GameManager.Instance.OnFruitClicked(this, normalizedX, transform.position);
    }

    public void DestroyFruit()
    {
        if (!isAlive) return;
        isAlive = false;
        Destroy(gameObject);
    }
}