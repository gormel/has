using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomTileset : MonoBehaviour
{
    public Texture2D Source;
    public int HorizontalSprites;
    public int VerticalSprites;

    public List<Sprite> Tiles { get; } = new List<Sprite>();

    void Awake()
    {
        var w = Source.width / HorizontalSprites;
        var h = Source.height / VerticalSprites;

        for (int y = 0; y < VerticalSprites; y++)
        {
            for (int x = 0; x < HorizontalSprites; x++)
            {
                Tiles.Add(Sprite.Create(Source, new Rect(x * w, y * h, w, h), Vector2.one / 2));
            }
        }
    }
}
