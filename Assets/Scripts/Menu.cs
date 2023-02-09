using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private EnterMenu _enterMenu;
    [SerializeField] private LobbyMenu _lobbyMenu;

    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);

    private void Start()
    {
        _enterMenu.gameObject.SetActive(false);
        _lobbyMenu.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        _enterMenu.gameObject.SetActive(true);
        JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        ShowMenu(_lobbyMenu.gameObject);
        _lobbyMenu.photonView.RPC("ListUpdate", RpcTarget.All);
    }

    public void ShowMenu(GameObject menu)
    {
        _enterMenu.gameObject.SetActive(false);
        _lobbyMenu.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        _lobbyMenu.ListUpdate();
    }

    public void LeaveLobby()
    {
        NetworkManager.Instance.LeaveLobby();
        ShowMenu(_enterMenu.gameObject);
    }

    public void StartGame(string sceneName)
    {
        NetworkManager.Instance.photonView.RPC("StartGame", RpcTarget.All, sceneName);
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }
}
