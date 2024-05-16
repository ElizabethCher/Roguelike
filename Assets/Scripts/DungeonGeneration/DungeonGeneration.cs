using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    // Start is called before the first frame update
    void Start()
    {
        dungeonRooms = DungeonCrullerControll.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        if(LevelUp.Level==1)
        RoomControll.instance.LoadRoom("Start", 0, 0);
        else 
            RoomControll.instance.LoadRoom("Start2", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomControll.instance.LoadRoom(RoomControll.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
           
        }
    }

}
