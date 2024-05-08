using TMPro;
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
    public TMP_Text CoinText;
    public static int coin = 0;
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
        CoinText.text = ": " + coin;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //���� ���������� � ���-�� � ���� tag == "Player"
        {
            PlayerController.collectedAmount++;
            coin += coinChange;
            GameControll.HealPlayer(healthChange);
            Destroy(gameObject);
        }
    }
}
