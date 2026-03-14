using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    [SerializeField]
    public void DepositMoney(int money)
    {
        DataSystem<DataController>.SaveMoney((DataSystem<DataController>.LoadMoney() + money));
    }

    public void DepositMoney()
    {
        if (GameController.Instance != null)
        {
            print(GameController.Instance.GetEarnedMoney());
            DataSystem<DataController>.SaveMoney((DataSystem<DataController>.LoadMoney() + GameController.Instance.GetEarnedMoney()));
        }
    }

    public int Withdrawals()
    {
        return DataSystem<DataController>.LoadMoney();
    }
}
