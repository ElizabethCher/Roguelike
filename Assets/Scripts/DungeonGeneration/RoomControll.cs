using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo   //информация о комнатах
{
    public string name;
    public int x;
    public int y;
}
public class RoomControll : MonoBehaviour
{
    public static RoomControll instance;
    string currentWorldName = "Basement"; //текущее имя мира
    RoomInfo currentLoadRoomData; //данные о текущей загруженной комнаты
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRoom = new List<Room>();
   Room currRoom;
    bool isLoadRoom = false;    //загружена ли эта комната
    bool spawnedBoosRoom = false;
    bool updatedRooms =false;

    private void Awake()
    {
        instance= this;
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraControll.instance.currRoom= room;
        currRoom = room;
    }
    public bool DoesRoomExist(int x, int y) //проверяем существует ли такая комната
    {
        return loadedRoom.Find(item => item.x == x && item.y == y) != null;
    }
    public Room FindRoom(int x, int y) //проверяем существует ли такая комната
    {
        return loadedRoom.Find(item => item.x == x && item.y == y);
    }
    // Start is called before the first frame update
    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
    }
    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x,y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;
        loadRoomQueue.Enqueue(newRoomData);
    }
    IEnumerator LoadRoomRoutime(RoomInfo info)
    {
        string roomName= currentWorldName+ info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (loadRoom.isDone==false)
        {
            yield return null;
        }
    }
    public void RegisterRoom(Room room)      
    {
        if (!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y))
        {
            room.transform.position = new Vector3(
                room.wigth * currentLoadRoomData.x,
                room.heigth * currentLoadRoomData.y,
                0
                );
            room.x = currentLoadRoomData.x;
            room.y = currentLoadRoomData.y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + ", " + room.y;
            room.transform.parent = transform;
            isLoadRoom = false;
            if (loadedRoom.Count == 0)
            {
                CameraControll.instance.currRoom = room;
            }     

            loadedRoom.Add(room);

        }
        else
        {
            Destroy(room.gameObject); ;
            isLoadRoom= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()//обновлять очередь
    {
        if (isLoadRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBoosRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBoosRoom && !updatedRooms)
            {
                foreach (Room room in loadedRoom)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms= true;
            }
            return;
        }
            currentLoadRoomData = loadRoomQueue.Dequeue();
            isLoadRoom = true;
        StartCoroutine(LoadRoomRoutime(currentLoadRoomData));

    }
    IEnumerator SpawnBossRoom()
    {
        spawnedBoosRoom= true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRoom[loadedRoom.Count-1];
            Room tempRoom = new Room(bossRoom.x, bossRoom.y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRoom.Single(r=> r.x==tempRoom.x && r.y==tempRoom.y);
            loadedRoom.Remove(roomToRemove);
            LoadRoom("End", tempRoom.x, tempRoom.y);
        }
    }

}
