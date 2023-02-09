using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string _prefabLocation;
    
    private int _playersCount = 0;
    public List<CustomPlayerControl> _players = new List<CustomPlayerControl>();

    public string winnerName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        //_players = new List<CustomPlayerControl>();
    }

    [PunRPC]
    private void AddPlayer()
    {
        _playersCount += 1;

        if (_playersCount == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        var playerObject = PhotonNetwork.Instantiate(_prefabLocation, new Vector3(Random.Range(-4f, 4f), 7f, Random.Range(-4f, 4f)), Quaternion.identity);
        var player = playerObject.GetComponent<CustomPlayerControl>();

        player.photonView.RPC("StartPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public void ResetPressed()
    {
        photonView.RPC("ResetGame", RpcTarget.All);
    }

    [PunRPC]
    public void ResetGame()
    {
        //print(_players.Count);

        //foreach (var player in _players)
        //{
        //    player.alive = true;
        //    if (player.photonView.IsMine)
        //    {
        //        print("É meu");
        //        player.transform.position = Vector3.up * 6;
        //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    } else
        //    {
        //        print("Não é meu");
        //    }
        //}
        _playersCount = 0;
        _players.Clear();
    }

    public void PlayerDied(CustomPlayerControl player)
    {
        int playersAlive = _players.Where(x => x.alive == true).ToList().Count;
        print("Players in game: " + playersAlive);
        _players.First(x => x == player).alive = false;
        playersAlive = _players.Where(x => x.alive == true).ToList().Count;
        print("Players in game: " + playersAlive);

        if (playersAlive <= 1)
        {
            if (_players.Any(x => x.alive == true))
            {
                winnerName = _players.First(x => x.alive == true).playerName;
            } else
            {
                winnerName = "EMPATOU";
            }

            print("Vencedor: " + winnerName);
            photonView.RPC("GameOver", RpcTarget.All, winnerName);
        }
    }

    [PunRPC]
    public void GameOver(string winnerName)
    {
        this.winnerName = winnerName;
        SceneManager.LoadScene("GameOver");
    }
}
