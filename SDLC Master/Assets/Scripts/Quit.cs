using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void Quitgame(){
        Application.Quit();
    }

    public void ToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
