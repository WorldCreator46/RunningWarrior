using UnityEngine;
using UnityEngine.UI;

public class SetClearImage : MonoBehaviour
{
    Text Title;
    Image Finish;
    Image Coin;
    Image Equipment;
    GameObject SaveObject;
    void Start()
    {
        Title = transform.Find("Title").GetComponent<Text>();
        Finish = transform.Find("ClearImage").transform.Find("Finish").GetComponent<Image>();
        Coin = transform.Find("ClearImage").transform.Find("Coin").GetComponent<Image>();
        Equipment = transform.Find("ClearImage").transform.Find("Equipment").GetComponent<Image>();
        SaveObject = GameObject.Find("SaveObject(Clone)");
        SaveObject.GetComponent<SaveFile>().ClearStageCheck(Title.text, Finish, Coin, Equipment);
    }
}
