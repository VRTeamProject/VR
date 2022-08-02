using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPositiopn = new Vector3(PhotonNetwork.LocalPlayer.ActorNumber, 0, 0);
        GameObject playerTemp = PhotonNetwork.Instantiate("Player", spawnPositiopn, Quaternion.identity, 0);
        playerTemp.name = FindObjectOfType<LobbyManager>().userID;
    }
}
