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
    private PhotonManager photonManager;

    private XROrigin xrOrigin;
    public VRRig vrRig;

    [SerializeField] private TextMeshProUGUI userAuth;
    [SerializeField] private TextMeshProUGUI userName;

    public float turnSmoothness;


    public Transform cameraPivot;
    public Transform uiCanvas;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            photonManager = PhotonManager.Instance;
            Initialize();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }
    }
    private void Initialize()
    {
        photonView.RPC(nameof(SendPlayerInfo), RpcTarget.All, photonManager.userID, photonManager.userAuth);

        transform.localPosition = Vector3.zero;
        XROriginHandler xrOriginHandler = FindObjectOfType<XROriginHandler>();
        vrRig.head.vrTarget = xrOriginHandler.head;
        vrRig.leftHand.vrTarget = xrOriginHandler.leftHand;
        vrRig.rightHand.vrTarget = xrOriginHandler.rightHand;
    }

    private void FindXROrigin()
    {
        xrOrigin = FindObjectOfType<XROrigin>();
    }

    [PunRPC]
    public void SendPlayerInfo(string userName, string userAuth)
    {
        this.userName.text = userName;
        this.userAuth.text = userAuth;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(xrOrigin.transform.position);

            stream.SendNext(vrRig.head.rigTarget.position);
            stream.SendNext(vrRig.leftHand.rigTarget.position);
            stream.SendNext(vrRig.rightHand.rigTarget.position);
            stream.SendNext(transform.position);

            stream.SendNext(vrRig.head.rigTarget.rotation);
            stream.SendNext(vrRig.leftHand.rigTarget.rotation);
            stream.SendNext(vrRig.rightHand.rigTarget.rotation);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();

            this.vrRig.head.rigTarget.position = (Vector3)stream.ReceiveNext();
            this.vrRig.leftHand.rigTarget.position = (Vector3)stream.ReceiveNext();
            this.vrRig.rightHand.rigTarget.position = (Vector3)stream.ReceiveNext();
            this.transform.position = (Vector3)stream.ReceiveNext();

            this.vrRig.head.rigTarget.rotation = (Quaternion)stream.ReceiveNext();
            this.vrRig.leftHand.rigTarget.rotation = (Quaternion)stream.ReceiveNext();
            this.vrRig.rightHand.rigTarget.rotation = (Quaternion)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
