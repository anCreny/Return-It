using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    private Scene target;
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }


}

