using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomsList : MonoBehaviourPunCallbacks
{
    public GameObject roomButtonModel;
    public GameObject container;
    public EnterMenu enterMenu;
    List<RoomInfo> roomList = new List<RoomInfo>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Room updated");
        this.roomList.Clear();
        foreach (var room in roomList)
        {
            this.roomList.Add(room);
            print(room.Name);
        }
        UpdateList();
    }

    public void UpdateList()
    {
        foreach (Transform button in container.transform)
        {
            Destroy(button.gameObject);
        }
        foreach (var room in roomList)
        {
            if (room.IsOpen && room.PlayerCount > 0)
            {
                Instantiate(roomButtonModel, container.transform).GetComponent<RoomButton>().Setup(room.Name, enterMenu);
            }
        }
    }
}
