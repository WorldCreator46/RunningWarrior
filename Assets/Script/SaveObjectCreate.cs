using UnityEngine;

public class SaveObjectCreate : MonoBehaviour
{
    public GameObject SaveObject;
    void Awake()
    {
        if(GameObject.Find("SaveObject") == null)
        {
            Instantiate(SaveObject);
        }
    }
}
