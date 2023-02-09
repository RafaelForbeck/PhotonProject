using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody))]
public class CustomPlayerControl : MonoBehaviourPunCallbacks
{
    private Rigidbody rig;
    public float speed;
    public float normalSpeed;
    public float powerSpeed;
    public float jumpForce;

    float powerTime = 8;
    float currentPowerTime = 0;
    bool isPowerUpActive = false;

    private Player _photonPlayer;
    private int _id;
    public GameObject explosion;
    public string playerName;

    public int Id { get => _id; set => _id = value; }

    public bool alive = true;

    [PunRPC]
    public void StartPlayer(Player player)
    {
        print("Start Player");
        _photonPlayer = player;
        Id = player.ActorNumber;
        playerName = player.NickName;
        print(GameManager.Instance._players.Count);
        GameManager.Instance._players.Add(this);
        print(GameManager.Instance._players.Count);
    }

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        powerSpeed = 3 * speed;
        normalSpeed = speed;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            photonView.RPC("Explode", RpcTarget.All, Id);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = Vector3.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rig.AddForce(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rig.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rig.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.AddForce(Vector3.right * speed * Time.deltaTime);
        }

        if (isPowerUpActive)
        {
            currentPowerTime += Time.deltaTime;
            if (currentPowerTime > powerTime)
            {
                RemovePowerUp();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
            rig.mass = 4;
            speed = powerSpeed;
            Destroy(collision.gameObject);
            currentPowerTime = 0;
            isPowerUpActive = true;
        } else if (collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);
            photonView.RPC("Explode", RpcTarget.All, Id);
        } else if (collision.gameObject.layer == 11)
        {
            if (NetworkManager.Instance.IsHost())
            {
                GameManager.Instance.PlayerDied(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (NetworkManager.Instance.IsHost())
            {
                GameManager.Instance.PlayerDied(this);
            }
        }
    }

    private void RemovePowerUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        rig.mass = 1;
        speed = normalSpeed;
        isPowerUpActive = false;
    }

    [PunRPC]
    public void Explode(int id)
    {
        if (id != Id)
        {
            return;
        }
        Instantiate(explosion, transform.position, Quaternion.identity).GetComponent<Explosion>().SetOwnerId(id);
    }
}
