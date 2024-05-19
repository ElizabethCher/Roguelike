using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public enum EnemyState  //состояние противника
    {
        Idle,
        Wander, //исследует
        Follow, //преследует
        Die, //погиб
        Attack  //аттакует
    };
    public enum EnemyType   //тип противника
    {
        Bos,
        Melee,  //противник ближнего боя
        Ranged  //противник дальнего боя
    };

    public int health;
    public bool notInRoom = false;
    public GameObject bulletPrefad; //пуля
    GameObject player; //игрок
    public EnemyState currState= EnemyState.Idle; //по умолчанию враг стоит
    public EnemyType enemyType;
    public float range; //расстояние на котором враг видит
    public float speed; //скорость противника
    public float coolDown;  //время восстановления
    public float attackingRange;
    private bool choostDir=false;   //враг выбрал направление
    private bool coolDownAttack=false;  //восстановление аттаки
    //private bool dead = false;  //враг мертв
    private Vector3 randomDir;  //случайное направление
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
            //если игрок в диапозоне врага
            if (IsPlayerRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow; //то враг начинает преследовать
            }
            else if (!IsPlayerRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander; //то враг продолжает гулять
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
    private bool IsPlayerRange(float range) //преследовать или нет
    {
        //проверяем находится ли игнор в радиусе действия
        return Vector3.Distance(transform.position, player.transform.position)<=range;
    }
    private IEnumerator ChooseDirection()
    {
        choostDir= true;
        //чуть чуть ждем и вращаемся
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        //randomDir = new Vector3(0,0, Random.Range(0, 360));
        ////куда-то идем
        //Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        choostDir = false;
    }
    void Wander()
    {
        //если мы не выбрали направление, то выбираем
        if (!choostDir)
        {
            StartCoroutine(ChooseDirection());
        }
        //враг идет
        transform.position += transform.right * speed * Time.deltaTime;
        //проверяем в досигаемости ли 
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
                    GameControll.DamagePlayer(1);   //наносит аттаку равную 1 единиуе
                    StartCoroutine(CoolDown());
                    break;
                case EnemyType.Ranged:
                    GameObject bullet = Instantiate(bulletPrefad, transform.position, Quaternion.identity) as GameObject; //игровой объект пули
                    bullet.GetComponent<BulletControll>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletControll>().isEnemyBullet = true; //вражеская я ли пуля
                    StartCoroutine(CoolDown());
                    break;
                case EnemyType.Bos:
                    bullet = Instantiate(bulletPrefad, transform.position, Quaternion.identity) as GameObject; //игровой объект пули
                    bullet.GetComponent<BulletControll>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletControll>().isEnemyBullet = true; //вражеская я ли пуля
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
