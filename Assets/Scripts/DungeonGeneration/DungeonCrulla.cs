using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrulla : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public DungeonCrulla(Vector2Int startPos)
    {
        Position= startPos;
    }
    public Vector2Int Move (Dictionary <Direction, Vector2Int> directionMovementMap)    //метод движения
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];   //выбираем сучайное направление
        return Position;
    }

}
