using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void SceneMove()
    {
        SceneManager.LoadScene(gameObject.GetComponent<Text>().text);
    }
}
