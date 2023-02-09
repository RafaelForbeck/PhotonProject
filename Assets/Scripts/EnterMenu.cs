using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterMenu : MonoBehaviour
{
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _roomName;

    public void CreateRoom()
    {
        NetworkManager.Instance.ChangeNickName(_playerName.text);
        NetworkManager.Instance.CreatedRoom(_roomName.text);
    }

    public void EnterRoom(string roomName)
    {
        NetworkManager.Instance.ChangeNickName(_playerName.text);
        NetworkManager.Instance.EnterRoom(roomName);
    }

}
