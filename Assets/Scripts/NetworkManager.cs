using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexão realizada com sucesso");
    }

    public void CreatedRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    internal void EnterRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeNickName(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public string GetPlayersList()
    {
        var list = "";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            list += player.NickName + "\n";
        }
        return list;
    }

    public bool IsHost()
    {
        return PhotonNetwork.IsMasterClient;
    }

    internal void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    internal void StartGame(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
