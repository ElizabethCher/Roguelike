using UnityEngine;

[System.Serializable]
public class Item
{
    public string name; //���
    public string description;  //��������
    public Sprite itemImage;
}

public class CollactionContril : MonoBehaviour
{
    public Item item;
    public int healthChange; //��������� ��������� ��������
    public int coinChange; //��������� ��������� ��������
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite=item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //���� ���������� � ���-�� � ���� tag == "Player"
        {
            PlayerController.collectedAmount++;
            GameControll.HealPlayer(healthChange);
            Destroy(gameObject);
        }
    }
}
