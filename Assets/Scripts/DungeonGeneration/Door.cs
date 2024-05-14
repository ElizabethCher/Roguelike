using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    };
    public DoorType doorType;
    private GameObject player;
    public GameObject doorCollider;
    private float withOffset = 15f;
    private float withOffset1 = 8f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            switch(doorType)
            {
                case DoorType.left:
                    player.transform.position = new Vector3(transform.position.x-withOffset, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector3(transform.position.x+ withOffset, transform.position.y);
                    break;
                case DoorType.bottom:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y - withOffset1);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + withOffset1);
                    break;
            }
        }
    }
}
