using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] int stageIndex;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(stageIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
