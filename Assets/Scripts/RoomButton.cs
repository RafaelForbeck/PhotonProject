using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text nameText;
    private string roomName;
    private EnterMenu enterMenu;

    public void Setup(string roomName, EnterMenu enterMenu)
    {
        this.roomName = roomName;
        this.enterMenu = enterMenu;
        nameText.text = roomName;
    }

    public void ButtonPressed()
    {
        enterMenu.EnterRoom(roomName);
    }
}
