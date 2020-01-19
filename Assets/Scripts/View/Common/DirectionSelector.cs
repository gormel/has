using UnityEngine;
using System.Collections;

public class DirectionSelector : MonoBehaviour
{
    public GameObject Left;
    public GameObject Up;
    public GameObject Right;
    public GameObject Down;

    public Vector2 Direction;
    public bool Enabled = true;

    void Update()
    {
        if (Direction != Vector2.zero)
            Direction.Normalize();

        Left.SetActive(Enabled && Vector2.Dot(Direction, Vector2.left) > 0.7);
        Up.SetActive(Enabled && Vector2.Dot(Direction, Vector2.up) > 0.7);
        Right.SetActive(Enabled && Vector2.Dot(Direction, Vector2.right) > 0.7);
        Down.SetActive(Enabled && Vector2.Dot(Direction, Vector2.down) > 0.7);
    }
}
