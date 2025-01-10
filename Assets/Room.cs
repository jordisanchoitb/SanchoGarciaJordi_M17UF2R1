using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int Position { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool IsShop { get; set; }
    public bool IsSpecial { get; set; }

    public Vector2Int Center => new Vector2Int(Position.x + Width / 2, Position.y + Height / 2);

    public Room(Vector2Int position, int width, int height)
    {
        Position = position;
        Width = width;
        Height = height;
        IsShop = false;
        IsSpecial = false;
    }
}

