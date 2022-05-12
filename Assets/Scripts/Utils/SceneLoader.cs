using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Awake ()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
