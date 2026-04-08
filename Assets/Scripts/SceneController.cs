using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToVictoryScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene(3);
    }
}
