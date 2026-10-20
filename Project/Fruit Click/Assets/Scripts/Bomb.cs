using System.Collections;
using UnityEngine;

public class Bomb : ClickableBehaviour
{
    [SerializeField]
    private float timeToLive = 2f;

    [SerializeField]
    private int pointsToBeDeducted = 10;

    private void Start()
    {
        StartCountDownCoroutine();
    }

    protected override int ClickPont()
    {
        return -pointsToBeDeducted;
    }

    protected override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(timeToLive);

        Destroy();
    }
}
