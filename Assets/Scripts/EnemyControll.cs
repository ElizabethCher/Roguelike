using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public enum EnemyState  //��������� ����������
    {
        Idle,
        Wander, //���������
        Follow, //����������
        Die, //�����
        Attack  //��������
    };
    public enum EnemyType   //��� ����������
    {
        Bos,
        Melee,  //��������� �������� ���
        Ranged  //��������� �������� ���
    };

    public int health;
    public bool notInRoom = false;
    public GameObject bulletPrefad; //����
    GameObject player; //�����
    public EnemyState currState= EnemyState.Idle; //�� ��������� ���� �����
    public EnemyType enemyType;
    public float range; //���������� �� ������� ���� �����
    public float speed; //�������� ����������
    public float coolDown;  //����� ��������������
    public float attackingRange;
    private bool choostDir=false;   //���� ������ �����������
    private bool coolDownAttack=false;  //�������������� ������
    //private bool dead = false;  //���� �����
    private Vector3 randomDir;  //��������� �����������
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Wander:
                Wander();
                break;
             case EnemyState.Follow:
                Follow();
                break;
             case EnemyState.Die:
                Death();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
        if (!notInRoom)
        {
            //���� ����� � ��������� �����
            if (IsPlayerRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow; //�� ���� �������� ������������
            }
            else if (!IsPlayerRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander; //�� ���� ���������� ������
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackingRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState=EnemyState.Idle;
        }

    }
    private bool IsPlayerRange(float range) //������������ ��� ���
    {
        //��������� ��������� �� ����� � ������� ��������
        return Vector3.Distance(transform.position, player.transform.position)<=range;
    }
    private IEnumerator ChooseDirection()
    {
        choostDir= true;
        //���� ���� ���� � ���������
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        //randomDir = new Vector3(0,0, Random.Range(0, 360));
        ////����-�� ����
        //Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        choostDir = false;
    }
    void Wander()
    {
        //���� �� �� ������� �����������, �� ��������
        if (!choostDir)
        {
            StartCoroutine(ChooseDirection());
        }
        //���� ����
        transform.position += transform.right * speed * Time.deltaTime;
        //��������� � ������������ �� 
        if (IsPlayerRange(range))
        {
            currState = EnemyState.Follow;
        }
    }
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }
    public void Death()
    {
        health -= 1;
        if (health <= 0 && enemyType==EnemyType.Bos)
        {
            RoomControll.instance.StartCoroutine(RoomControll.instance.RoomCoroutine());
            Destroy(gameObject);
            GameControll.instance.port.SetActive(true);
            GameControll.instance.port.transform.position =transform.position;
        }
        else if (health <= 0) 
        {
            RoomControll.instance.StartCoroutine(RoomControll.instance.RoomCoroutine());
            Destroy(gameObject);
        }

    }
    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case EnemyType.Melee:
                    GameControll.DamagePlayer(1);   //������� ������ ������ 1 �������
                    StartCoroutine(CoolDown());
                    break;
                case EnemyType.Ranged:
                    GameObject bullet = Instantiate(bulletPrefad, transform.position, Quaternion.identity) as GameObject; //������� ������ ����
                    bullet.GetComponent<BulletControll>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletControll>().isEnemyBullet = true; //��������� � �� ����
                    StartCoroutine(CoolDown());
                    break;
                case EnemyType.Bos:
                    bullet = Instantiate(bulletPrefad, transform.position, Quaternion.identity) as GameObject; //������� ������ ����
                    bullet.GetComponent<BulletControll>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletControll>().isEnemyBullet = true; //��������� � �� ����
                    StartCoroutine(CoolDown());
                    break;
            }

        }
    }
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

}
