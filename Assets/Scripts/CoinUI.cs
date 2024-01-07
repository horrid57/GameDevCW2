using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public void UpdateCount(int coins) {
        GetComponent<TextMeshProUGUI>().text = coins.ToString();
    }
}
