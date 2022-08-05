using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public List<Transform> spawnPoints;
    private void Awake()
    {
        
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPositiopn = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % 3].position;
        GameObject playerTemp = PhotonNetwork.Instantiate("Player", spawnPositiopn, Quaternion.identity, 0);
        playerTemp.name = FindObjectOfType<PhotonManager>().userID;
    }
}
