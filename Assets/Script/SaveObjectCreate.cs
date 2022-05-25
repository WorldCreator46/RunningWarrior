using UnityEngine;

public class SaveObjectCreate : MonoBehaviour
{
    public GameObject SaveObject;
    void Start()
    {
        if(GameObject.Find("SaveObject") == null)
        {
            Instantiate(SaveObject);
        }
    }
}
