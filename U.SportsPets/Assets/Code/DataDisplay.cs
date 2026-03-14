using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMoney;

    private void Start()
    {
        ShowMoney();
    }

    private void ShowMoney()
    {
        textMoney.text = ""+DataSystem<DataDisplay>.LoadMoney();
    }
}
