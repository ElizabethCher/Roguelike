using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up =0,
    down =1,
    left =2,
    right =3,
};
public class DungeonCrullerControll : MonoBehaviour
{
    public static List<Vector2Int> positionVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up },
         {Direction.down, Vector2Int.down },
          {Direction.left, Vector2Int.left },
           {Direction.right, Vector2Int.right}
    };
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData) //метод генерации 
    {
        List<DungeonCrulla> dungeonCrawlers = new List<DungeonCrulla>();
        for (int i = 0; i < dungeonData.nemberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrulla(Vector2Int.zero));
        }
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);
        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrulla dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionVisited.Add(newPos);
            }
        }
        return positionVisited;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
