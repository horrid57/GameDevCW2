using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public void RemoveRock() {
        gameObject.SetActive(false);
    }
}