using UnityEngine;

[CreateAssetMenu(fileName = "NewFruit", menuName = "Fruit Click/Fruit Data")]
public class FruitData : ScriptableObject
{
    public Sprite sprite;
    public int basePoints;       // points when clicked in the middle zone
    public float timeToLive;     // seconds before disappearing (shorter = less points)
    public float weight = 1f;    // for weighted random (higher = more common)
}