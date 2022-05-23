using UnityEngine;

public class StageSelectPanelControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
