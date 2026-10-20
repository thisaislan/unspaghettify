using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : ClickableBehaviour
{
    public Image fruitImage;
    public Image progressBar;

    private  FruitData fruitData;

    protected override int ClickPont()
    {
        return fruitData.basePoints;
    }

    protected override IEnumerator StartCountDown()
    {
        float elapsed = 0f;

        while (isAlive)
        {
            if (GameManager.Instance == null || Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            elapsed += Time.deltaTime;

            float progress = elapsed / fruitData.timeToLive;

            progressBar.fillAmount -= progress;

            if (progressBar.fillAmount <= 0f)
            {
                // Fruit disappears without points
                Destroy();
                yield break;
            }

            yield return null;
        }
    }

    public void Initialize(FruitData fruitData)
    {
        this.fruitData = fruitData;
        fruitImage.sprite = fruitData.sprite;
        progressBar.fillAmount = 1f;

        // Start timer
        isAlive = true;

        StartCountDownCoroutine();
    }
}
