using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameControll : MonoBehaviour
{
    public static GameControll instance;
    //public TMP_Text healthText;
    public TMP_Text dead;
    public TMP_Text level;
    private static int health=8;  //��������
    private static int maxHealth=8;   //max ��������
    //private static float moveSpeed=5f; 
    //private static float fireRate=0.5f;  
    public static int Health { get => health; set => health=value; }
    public static int MaxHealth { get => maxHealth; set => MaxHealth = value; }
    //public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    //public static float FireRate { get => fireRate; set => fireRate = value; }

    // Awake ���������� ���� ��� ��� �������� ���������� �������.
    // �� ���������� ����� ������� Start � ������������ ���
    // ������������� ����� ���������� ��� ������� ��������, ����������� ����� �������� ����
    private void Awake()
    {
        health = maxHealth;
        if (instance==null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (LevelUp.Level > 1)
        //{
        //    level.text = $"�������: {LevelUp.Level-1}";
        //}
        //else
        level.text = $"�������: {LevelUp.Level}";

    }
    public static void DamagePlayer(int damage) //�����, ����� �������� ����
    {
            health -= damage;
        if (Health<=0)
        {
            KillPlayer();
        }
    }
    public static void HealPlayer(int healAmount) //�����, ����� �������������� ����
    {
        health=Mathf.Min(maxHealth, health+healAmount);
    }
    private static void KillPlayer()    //����� �����
    {
        LevelUp.Level = 1;
        GameControll.instance.dead.gameObject.SetActive(true);
        new WaitForSeconds(20f);
        SceneManager.LoadScene("SampleScene");
    }
}
