using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public static int Level = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Уровень: "+ Level);
        if (collision.tag == "Player" && Level == 1)
        {
            SceneManager.LoadScene("BasementMain2");
            RecordsControll.RecordWrite();
        }
        else if (collision.tag == "Player" && Level == 3)
        {
            SceneManager.LoadScene("SampleScene");
            RecordsControll.RecordWrite();
        }
        Level++;
    }

}
