using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet =false;
    private Vector2 lastPos; //последняя позиция
    private Vector2 curPos; //текущая позиция
    private Vector2 playerPos; //позиция игрока
    // Start is called before the first frame update
    void Start()
    {
        //При использовании оператора yield сопрограмма приостанавливает выполнение и автоматически возобновляется со следующего кадра
        StartCoroutine(DeatDelay());//задержка для контроллера пули
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet)
        {
            curPos= transform.position;
            transform.position= Vector2.MoveTowards(transform.position, playerPos, 5f*Time.deltaTime);
            if (curPos==lastPos) 
            { 
                Destroy(gameObject);
            }
            lastPos= curPos;
        }
    }
    public void GetPlayer(Transform player)
    {
        playerPos=player.position;
    }
    IEnumerator DeatDelay() //сопрограмма это метод, который может приостановить выполнение и вернуть управление Unity,
                            //но затем продолжить с того места, на котором оно было остановлено, в следующем фрейме. 
    {
        yield return new WaitForSeconds(lifeTime);  //Приостанавливает выполнение сопрограммы на заданное количество секунд
        Destroy(gameObject);    //уничтожает игровой объект

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyControll>().Death();
            Destroy(gameObject);
        }
        if (collision.tag == "Player" && isEnemyBullet)
        {
            GameControll.DamagePlayer(1);
            Destroy(gameObject);
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
