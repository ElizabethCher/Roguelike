using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet =false;
    private Vector2 lastPos; //��������� �������
    private Vector2 curPos; //������� �������
    private Vector2 playerPos; //������� ������
    // Start is called before the first frame update
    void Start()
    {
        //��� ������������� ��������� yield ����������� ���������������� ���������� � ������������� �������������� �� ���������� �����
        StartCoroutine(DeatDelay());//�������� ��� ����������� ����
        
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
    IEnumerator DeatDelay() //����������� ��� �����, ������� ����� ������������� ���������� � ������� ���������� Unity,
                            //�� ����� ���������� � ���� �����, �� ������� ��� ���� �����������, � ��������� ������. 
    {
        yield return new WaitForSeconds(lifeTime);  //���������������� ���������� ����������� �� �������� ���������� ������
        Destroy(gameObject);    //���������� ������� ������

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
