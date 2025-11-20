using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {   
        SceneManager.LoadScene("Level 1");
    }
}
