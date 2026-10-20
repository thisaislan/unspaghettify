using System.Collections;
using UnityEngine;

public abstract class ClickableBehaviour : MonoBehaviour
{
    protected bool isAlive = true;

    private Coroutine countDownCoroutine;

    protected abstract int ClickPont();

    protected abstract IEnumerator StartCountDown();

    protected virtual void OnDestroyed()
    {
        if (countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
    }

    protected virtual void OnMouseDownAction()
    {
        GameManager.Instance.OnClicked(this, ClickPont());
    }

    protected virtual void StartCountDownCoroutine()
    {
        countDownCoroutine = StartCoroutine(StartCountDown());
    }

    public void Destroy()
    {
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        
        OnDestroyed();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!isAlive)
        {
            return;
        }

        OnMouseDownAction();
    }
}