using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryStart : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
