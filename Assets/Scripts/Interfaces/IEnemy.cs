using UnityEngine;
using System.Collections;

public enum EnemyType
{
    Hunter,
    Farter,
    Licker,
    Smeller
}

public interface IEnemy
{
    int hp { get; set; }
    EnemyType type { get; }

    void Deactivate();
    void Activate();

    void OnMouseTouch();
    void Interact(GameObject go);
    void Setup(Vector2 pos, char value);
}
