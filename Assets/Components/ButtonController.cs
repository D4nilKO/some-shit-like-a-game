using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartPressed()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ExitPressed() 
    {
        print("Exit pressed!");
        Application.Quit();
    }
}
