using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo   //���������� � ��������
{
    public string name;
    public int x;
    public int y;
}
public class RoomControll : MonoBehaviour
{
    public static RoomControll instance;
    string currentWorldName = "Basement"; //������� ��� ����
    RoomInfo currentLoadRoomData; //������ � ������� ����������� �������
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRoom = new List<Room>();
   Room currRoom;
    bool isLoadRoom = false;    //��������� �� ��� �������
    bool spawnedBoosRoom = false;
    bool updatedRooms =false;
    public string GetRandomRoomName()
    {
        if (LevelUp.Level==1)
        {
            string[] possibleRooms = new string[]        {
            /*"Empty",*/ "Basic1", "Bas1"
        }; return possibleRooms[Random.Range(0, possibleRooms.Length)];
        }
        else
        {
            string[] possibleRooms = new string[]        {
            /*"Empty",*/ "Basic2", "Bas2"
        }; return possibleRooms[Random.Range(0, possibleRooms.Length)];
        }

    }

    private void Awake()
    {
        instance= this;
    }
    public void OnPlayerEnterRoom(Room room)
    {

        CameraControll.instance.currRoom= room;
        currRoom = room;
        StartCoroutine(RoomCoroutine());

    }
    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRoom();
    }
    public void UpdateRoom()
    {
        foreach (Room room in loadedRoom)
        {
            if (currRoom != room)
            {
                EnemyControll[] enemies = room.GetComponentsInChildren<EnemyControll>();
                if (enemies != null)
                {
                    foreach (EnemyControll enemy in enemies)
                    {
                        enemy.notInRoom = true;
                       //Debug.Log("Not in room");
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }
            else
            {
                EnemyControll[] enemies = room.GetComponentsInChildren<EnemyControll>();
                if (enemies /*!= null*/.Length > 0)
                {
                    foreach (EnemyControll enemy in enemies)
                    {
                        enemy.notInRoom = false;
                       // Debug.Log("In in room");
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }
        }
    }
    public bool DoesRoomExist(int x, int y) //��������� ���������� �� ����� �������
    {
        return loadedRoom.Find(item => item.x == x && item.y == y) != null;
    }
    public Room FindRoom(int x, int y) //��������� ���������� �� ����� �������
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
    void UpdateRoomQueue()//��������� �������
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
                UpdateRoom();
                updatedRooms = true;
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
            if (LevelUp.Level == 1)
            {
                LoadRoom("End", tempRoom.x, tempRoom.y);
            }
            else LoadRoom("End2", tempRoom.x, tempRoom.y);

        }
    }

}
