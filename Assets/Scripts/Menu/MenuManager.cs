using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject initiallyOpen;

    private GameObject m_Open;

    void OnEnable()
    {
        if (initiallyOpen == null)
            return;

        OpenPanel(initiallyOpen);
    }

    public void OpenPanel(GameObject panel)
    {
        if (m_Open == panel)
            return;

        panel.SetActive(true);
        panel.transform.SetAsLastSibling();

        CloseCurrent();
		
        m_Open = panel;
    }

    public void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetActive(false);
        m_Open = null;
    }

    public void SwitchPanels(GameObject fromPanel, GameObject toPanel)
    {
        if (fromPanel != null) fromPanel.SetActive(false);
        if (toPanel != null) toPanel.SetActive(true);
    }

    public void RestartActiveScene()
    {
        GameManager.Instance.RestartScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        GameManager.Instance.Quit();
    }

    public void SwitchToScene(string sceneName)
    {
        GameManager.Instance.SwitchToScene(sceneName);
    }
}
