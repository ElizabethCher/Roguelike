using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }

    }

}
