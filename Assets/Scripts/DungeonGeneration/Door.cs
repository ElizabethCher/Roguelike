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
    private float withOffset = 8f;
    private float withOffset1 = 15f;
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
                    player.transform.position = new Vector3(transform.position.x-withOffset1, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector3(transform.position.x+ withOffset1, transform.position.y);
                    break;
                case DoorType.bottom:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y - withOffset);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + withOffset);
                    break;
            }
        }
    }
}
