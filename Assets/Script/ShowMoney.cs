using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    Text MonetDisplay;
    public int PresentMoney = 0;
    void Start()
    {
        MonetDisplay = GetComponent<Text>();
    }
    public void AddMoney()
    {
        PresentMoney++;
        MonetDisplay.text = $"{PresentMoney}G";
    }
}
