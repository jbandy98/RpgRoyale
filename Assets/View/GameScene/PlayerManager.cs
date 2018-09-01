using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : Photon.MonoBehaviour, IPunObservable {
    [SerializeField]
    public PlayerData data;

    public TextMeshPro playerText;

    void Start()
    {
        playerText.text = data.playerName;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(data.playerName);
        }
        else
        {
            data.playerName = (string)stream.ReceiveNext();           
        }
        playerText.text = data.playerName;
    }
}
