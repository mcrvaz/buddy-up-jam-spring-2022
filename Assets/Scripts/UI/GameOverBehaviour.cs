using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject GameOverPanel;
    [field: SerializeField] public Button RetryButton;

    PlayerBehaviour playerBehaviour;

    void Awake ()
    {
        RetryButton.onClick.AddListener(HandleRetryClick);

        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

        GameOverPanel.SetActive(false);
    }

    void Start ()
    {
        playerBehaviour.Player.OnDeath += HandlePlayerDeath;
    }

    void HandlePlayerDeath ()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameOverPanel.SetActive(true);
    }

    void HandleRetryClick ()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name, LoadSceneMode.Single);
    }
}
