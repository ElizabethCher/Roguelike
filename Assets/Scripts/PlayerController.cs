using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //скорость
    new Rigidbody2D rigidbody;  //определ€ем твердое тело
    public TMP_Text collectionText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefad; //пул€
    public float bulletSpeed; //скорость пули
    public float lastFire; //последн€€ пул€
    public float fireDelay; //задержка пули
    // Start is called before the first frame update
    void Start()
    {
        //возвращает компонент, прикреплЄнный к GameObject, и все его текущие свойства
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //горизонталь ходьбы
        float vertical = Input.GetAxis("Vertical"); //вертикаль ходьбы

        float shootHorizontal = Input.GetAxis("ShootHorizontal");   //горизонталь стрельбы
        float shootVertical = Input.GetAxis("ShootVertical");   //вертикаль стрельбы

        //провер€ем получаем ли мы горизонтальные или вертикальные входные данные
        //и больше ли чем наша последнн€€ задержка огн€ + врем€ нашего последнего огн€
        if ((shootHorizontal!=0 || shootVertical!=0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire= Time.time;//мен€ем врем€ последнего огн€
        }
        rigidbody.velocity= new Vector3(horizontal*speed, vertical*speed, 0);   //скорость равна€ вектору 3
        //collectionText.text = "Items Collected: " +collectedAmount;
    }

    void Shoot(float x , float y) //метод стрельбы
    {
        GameObject bullet = Instantiate(bulletPrefad, transform.position /*позици€ точки*/, transform.rotation) as GameObject; //пул€
        bullet.AddComponent<Rigidbody2D>().gravityScale=0;//твердое тело c 0 гравитацией 
        bullet.GetComponent<Rigidbody2D>().velocity= new Vector3(
            (x<0)? Mathf.Floor(x)* bulletSpeed: Mathf.Ceil(x)*bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0); //скорость равнаа€ новому вектору 3
    }

}
