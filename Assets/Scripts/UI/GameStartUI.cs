using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    void Update ()
    {
        if (InputManager.Instance.GetAnyKeyDown())
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}