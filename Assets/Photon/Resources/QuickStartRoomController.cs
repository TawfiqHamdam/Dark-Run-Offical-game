using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int firstLevelScene = 1; //Number for the build index to the multiplay scene

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }
    private void StartGame() //function for loading into multiplayer scene
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("starting game");
            PhotonNetwork.LoadLevel(firstLevelScene);
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
