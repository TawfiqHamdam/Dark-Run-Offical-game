using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _quickStartButton; //button used for creating a game
    [SerializeField]
    private GameObject _quickCancelButton; //button used to stop searching for game
    [SerializeField]
    private int _roomSize = 10; // set this to max amount of people in the room at one time

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _quickStartButton.SetActive(true);
    }

    public void QuickStart()
    {
        _quickStartButton.SetActive(false);
        _quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); // first tries to join a random room
        Debug.Log("Quick Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join room Failed");
        OnCreatedRoom();
    }

    void CreateRoom()
    {
        Debug.Log("creating room now");
        int randomRoomNumber = Random.Range(0, 1000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)_roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room...trying again please wait");
        CreateRoom();

    }
    public void QuickCancel()
    {
        _quickCancelButton.SetActive(false);
        _quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();

    }
}





   
