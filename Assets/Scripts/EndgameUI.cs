using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndgameUI : MonoBehaviour
{
    public GameObject panel;
    public Text title;
    public Button restartButton;
    public Button quitButton;
    [SerializeField] private string mainMenuSceneName = "SampleScene";
    [SerializeField] private Button mainMenuButton;
    void Awake()
    {
        if (mainMenuButton)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (panel) panel.SetActive(false);
        if (restartButton)
            restartButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            });
        if (quitButton)
            quitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }
    public void ShowWin()
    {
        if (!panel) { Debug.Log("YOU WIN"); return; }
        title.text = "YOU WIN!";
        panel.SetActive(true);
    }
    public void ShowLose()
    {
        if (!panel) { Debug.Log("YOU LOSE"); return; }
        title.text = "YOU LOSE!";
        panel.SetActive(true);
    }
}
