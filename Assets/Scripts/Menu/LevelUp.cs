using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public static int Level = 1;
    public static bool trig = true;

    private void Update()
    {
        trig=true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( trig)
        {
            if (collision.tag == "Player" && Level == 1)
            {
                SceneManager.LoadScene("BasementMain2");
                Level++;
            }
            else if (collision.tag == "Player" && Level == 2)
            {
                SceneManager.LoadScene("SampleScene");
            }
            RecordsControll.RecordWrite();


        }
        trig = false;

    }

}
