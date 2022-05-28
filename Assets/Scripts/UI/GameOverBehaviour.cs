using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class GameOverBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject GameOverPanel;
    [field: SerializeField] public Button RetryButton;

    [Inject]
    PlayerBehaviour playerBehaviour;

    void Awake ()
    {
        RetryButton.onClick.AddListener(HandleRetryClick);
        GameOverPanel.SetActive(false);
    }

    void Start ()
    {
        playerBehaviour.Player.OnDeath += HandlePlayerDeath;
    }

    void HandlePlayerDeath ()
    {
        CursorManager.SetVisible(true);
        GameOverPanel.SetActive(true);
    }

    void HandleRetryClick ()
    {
        SceneManager.LoadScene("Reload", LoadSceneMode.Single);
    }
}
