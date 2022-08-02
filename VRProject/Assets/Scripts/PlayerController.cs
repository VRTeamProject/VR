using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;

using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    private XROrigin xrOrigin;
    [SerializeField] private TextMeshProUGUI userName;

    private PlayerInput playerInput;
    private Vector2 moveDirection = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.PlayerControl.Move.performed += val => { moveDirection = val.ReadValue<Vector2>(); };
        playerInput.PlayerControl.Move.canceled += val => { moveDirection = Vector2.zero; };

        xrOrigin = FindObjectOfType<XROrigin>();
        photonView.RPC(nameof(SendPlayerInfo), RpcTarget.All, FindObjectOfType<LobbyManager>().userID);

    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * 5f;
            xrOrigin.transform.position = transform.position;
        }
    }

    private void LateUpdate()
    {
        
    }

    [PunRPC]
    public void SendPlayerInfo(string userName)
    {
        this.userName.text = userName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
           
    }
}
