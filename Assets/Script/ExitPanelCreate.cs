using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPanelCreate : MonoBehaviour
{
    public GameObject ExitPanel;
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && GameObject.Find("StageSelectPanel") == null && GameObject.Find("OptionPanel") == null && Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject _ExitPanel = Instantiate(ExitPanel, GameObject.Find("Canvas").transform);
            Destroy(_ExitPanel, 1.5f);
        }
    }
}
