using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("BasementMain");
    }
    public void ExitPressed()
    {
        SceneManager.LoadScene("RefinementScene");
        Application.Quit();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void NoExit()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
