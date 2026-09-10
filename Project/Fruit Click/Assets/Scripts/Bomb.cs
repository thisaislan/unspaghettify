using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timeToLive = 2f;
    private float elapsed = 0f;
    private bool isAlive = true;

    private void Update()
    {
        if (!isAlive) return;
        elapsed += Time.deltaTime;
        if (elapsed >= timeToLive)
        {
            DestroyBomb();
        }
    }

    private void OnMouseDown()
    {
        if (!isAlive) return;
        GameManager.Instance.OnBombClicked(this, transform.position);
    }

    public void DestroyBomb()
    {
        if (!isAlive) return;
        isAlive = false;
        Destroy(gameObject);
    }
}