using UnityEngine;

[CreateAssetMenu(fileName = "NewFruit", menuName = "Fruit Click/Fruit Data")]
public class FruitData : ScriptableObject
{
    public Sprite sprite;

    // points when clicked in the middle zone
    public int basePoints;

    // seconds before disappearing (shorter = less points)
    public float timeToLive;

    // for weighted random (higher = more common)
    public float weight = 1f;
}
