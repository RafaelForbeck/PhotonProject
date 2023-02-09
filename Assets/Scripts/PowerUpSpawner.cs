using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PowerUpSpawner : MonoBehaviourPunCallbacks
{
    bool isHost;

    public float interval;
    float currentTime = 15;

    public GameObject powerUpModel;
    public List<string> powerUpsNames;

    private void Start()
    {
        isHost = NetworkManager.Instance.IsHost();
    }

    private void Update()
    {
        if (isHost == false)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime >= interval)
        {
            currentTime -= interval;
            //photonView.RPC("SpawnPowerUp", RpcTarget.All);
            SpawnPowerUp();
        }
    }

    //[PunRPC]
    void SpawnPowerUp()
    {
        PhotonNetwork.Instantiate(powerUpsNames[Random.Range(0, powerUpsNames.Count)], transform.position, Quaternion.identity);
        //Instantiate(powerUpModel, transform.position, Quaternion.identity, transform);
    }
}
