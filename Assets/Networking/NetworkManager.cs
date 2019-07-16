using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {

    public string roomName = "TestServer";
    public Room room;
    public GameObject player;
    public List<Transform> spawnpoints;
    public Camera playerCamera;
    public bool connected = false;
    public int gameId;

    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;

    public byte Version = 1;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;

    // Use this for initialization
    void Start () {
        PhotonNetwork.autoJoinLobby = false;
        SetupPlayer();
    }

    void SetupPlayer()
    {
        gameId = 1;
        room = PhotonNetwork.room;
        Debug.Log("Connected to Room: " + room.Name);
        // pick a random spawn point
        int randomSpawnPoint = Random.Range(0, spawnpoints.Count);
        GameObject playerSpawn = PhotonNetwork.Instantiate(player.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        playerSpawn.GetComponent<PlayerController>().gameData.gameId = gameId;
        playerSpawn.GetComponent<PlayerController>().gameData.locX = (int)spawnpoints[randomSpawnPoint].transform.position.x;
        playerSpawn.GetComponent<PlayerController>().gameData.locY = (int)spawnpoints[randomSpawnPoint].transform.position.y;
        playerCamera.transform.SetParent(playerSpawn.transform);
        playerCamera.transform.localPosition.Set(playerSpawn.transform.position.x + 2.5f, playerSpawn.transform.position.y, playerSpawn.transform.position.z);
        playerSpawn.transform.position = spawnpoints[randomSpawnPoint].transform.position;
        PlayerController playerManager = playerSpawn.GetComponent<PlayerController>();
        playerManager.playerData.name = PlayerPrefs.GetString("user");
        playerManager.gameData.playerId = PlayerPrefs.GetString("user");
        playerManager.playerData.session = PlayerPrefs.GetString("sessionId");
    }

    private void Update()
    {
        if (connected)
        {
            return;
        }
        else
        {
            Reconnect();
        }
    }

    void Reconnect()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }
        else
        {
            Debug.Log("Already connected to game server in room: " + room.Name);
            connected = true;
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
        connected = true;
        Debug.Log("Connected to Room: " + room.Name);

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
        connected = false;
        Debug.LogError("Cause: " + cause);
    }

    public override void OnDisconnectedFromPhoton()
    {
        connected = false;
        Debug.Log("You have been disconnected from the game!");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        // if a player is disconnected from the game, what do we do to clean them up?
        Debug.Log("Player left the game: " + otherPlayer.userId);
    }
}
