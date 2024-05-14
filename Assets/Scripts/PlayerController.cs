using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //��������
    new Rigidbody2D rigidbody;  //���������� ������� ����
    public TMP_Text collectionText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefad; //����
    public float bulletSpeed; //�������� ����
    public float lastFire; //��������� ����
    public float fireDelay; //�������� ����
    // Start is called before the first frame update
    void Start()
    {
        //���������� ���������, ������������ � GameObject, � ��� ��� ������� ��������
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //����������� ������
        float vertical = Input.GetAxis("Vertical"); //��������� ������

        float shootHorizontal = Input.GetAxis("ShootHorizontal");   //����������� ��������
        float shootVertical = Input.GetAxis("ShootVertical");   //��������� ��������

        //��������� �������� �� �� �������������� ��� ������������ ������� ������
        //� ������ �� ��� ���� ���������� �������� ���� + ����� ������ ���������� ����
        if ((shootHorizontal!=0 || shootVertical!=0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire= Time.time;//������ ����� ���������� ����
        }
        rigidbody.velocity= new Vector3(horizontal*speed, vertical*speed, 0);   //�������� ������ ������� 3
        //collectionText.text = "Items Collected: " +collectedAmount;
    }

    void Shoot(float x , float y) //����� ��������
    {
        GameObject bullet = Instantiate(bulletPrefad, transform.position /*������� �����*/, transform.rotation) as GameObject; //����
        bullet.AddComponent<Rigidbody2D>().gravityScale=0;//������� ���� c 0 ����������� 
        bullet.GetComponent<Rigidbody2D>().velocity= new Vector3(
            (x<0)? Mathf.Floor(x)* bulletSpeed: Mathf.Ceil(x)*bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0); //�������� ������� ������ ������� 3
    }

}
