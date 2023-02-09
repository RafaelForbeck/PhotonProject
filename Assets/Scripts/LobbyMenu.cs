using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _playersList;
    [SerializeField] private Button _startButton;

    [PunRPC]
    public void ListUpdate()
    {
        _playersList.text = NetworkManager.Instance.GetPlayersList();
        _startButton.interactable = NetworkManager.Instance.IsHost();
    }
}
