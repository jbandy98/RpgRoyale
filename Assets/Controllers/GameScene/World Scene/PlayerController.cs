using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;

[RequireComponent(typeof(PhotonView))]
public class PlayerController : Photon.MonoBehaviour, IPunObservable {
    [SerializeField]
    public PlayerData playerData;

    public GameData gameData;

    public TextMeshPro playerText;

    void Start()
    {
        playerText.text = playerData.name;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playerData.name);
        }
        else
        {
            playerData.name = (string)stream.ReceiveNext();           
        }
        playerText.text = playerData.name;
    }

}
