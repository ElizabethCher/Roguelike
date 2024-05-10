using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int wigth;
    public int heigth;
    public int x;
    public int y;
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public List<Door> doors= new List<Door>();
    private bool updatedDoors = false;
    public Room (int x, int y)
    {
        this.x = x;
            this.y = y;
    }
    // Start is called before the first frame update
    void Start()
    {
        //проверяем находится ли комната в правильной сцене
        if (RoomControll.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }
        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
           doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor= d;
                    break;
                    case Door.DoorType.left:
                    leftDoor= d;
                    break;
                    case Door.DoorType.top:
                        topDoor= d;
                    break;
                    case Door.DoorType.bottom:
                    bottomDoor= d;
                    break;
            }
        }
        RoomControll.instance.RegisterRoom(this);
    }
    public void RemoveUnconnectedDoors()    //метод удаления не нужных дверей
    {
        foreach (Door d in doors)
        {
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    if (GetRight() == null)
                       d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        d.gameObject.SetActive(false);
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if (RoomControll.instance.DoesRoomExist(x +1, y))
        {
            return RoomControll.instance.FindRoom(x+1, y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomControll.instance.DoesRoomExist(x -1, y))
        {
            return RoomControll.instance.FindRoom(x- 1, y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomControll.instance.DoesRoomExist(x, y+1))
        {
            return RoomControll.instance.FindRoom(x, y+1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomControll.instance.DoesRoomExist(x, y-1))
        {
            return RoomControll.instance.FindRoom(x, y-1);
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(wigth, heigth, 0));
    }
    public Vector3 GetRoomCenter() //получение центра комнаты
    {
        return new Vector3(x*wigth, y*heigth);
    }
    // Update is called once per frame
    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors= true;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            RoomControll.instance.OnPlayerEnterRoom(this);
        }
    }
}
