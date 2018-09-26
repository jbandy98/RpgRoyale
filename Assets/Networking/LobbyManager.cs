using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LobbyManager : MonoBehaviour {
    public string roomName = "TestLobbyServer";
    public Room room;
    public string username;

    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = false;

    public byte Version = 1;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
        username = PlayerPrefs.GetString("user");
    }

    private void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("Getting room list.");
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log("Room list: " + rooms.ToString());
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRoom(roomName);");
        PhotonNetwork.JoinRoom(roomName);
    }

    void OnJoinedRoom()
    {
        room = PhotonNetwork.room;
        Debug.Log("Connected to Room: " + room.Name);
        GetComponent<LobbySceneController>().UpdatePlayerList();
    }

    public void ConnectToNetwork()
    {
        PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRoom(roomName);
    }

    public virtual void OnPhotonJoinRoomFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(roomName);
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public string GetPlayersList(Room room)
    {
        StringBuilder playerStr = new StringBuilder();
       foreach (var player in PhotonNetwork.playerList)
        {
            playerStr.Append(player.NickName + ", ");
        }
        Debug.Log("Player list being returned: " + playerStr.ToString());
        return playerStr.ToString();
    }
}
