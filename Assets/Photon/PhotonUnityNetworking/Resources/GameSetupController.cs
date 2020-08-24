using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        CreatePlayer(); // create networked player object for each player that loads into the multiplayer 
    }
    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
            
    }
    
   
}
