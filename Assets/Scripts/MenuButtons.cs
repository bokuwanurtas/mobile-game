using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("Level1");
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quitting the game");
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
