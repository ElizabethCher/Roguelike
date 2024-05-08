using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int wigth;
    public int heigth;
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        //проверяем находится ли комната в правильной сцене
        if (RoomControll.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }
        RoomControll.instance.RegisterRoom(this);
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
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            RoomControll.instance.OnPlayerEnterRoom(this);
        }
    }
}
