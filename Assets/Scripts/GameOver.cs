using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviourPunCallbacks
{
    public Text winner;

    void Start()
    {
        winner.text = "Vencedor: " + GameManager.Instance.winnerName;
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayAgainPressed()
    {
        photonView.RPC("PlayAgain", RpcTarget.All);
    }

    [PunRPC]
    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("GameScene");
    }
}
