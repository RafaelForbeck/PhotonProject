using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.StartGame();
    }
}
