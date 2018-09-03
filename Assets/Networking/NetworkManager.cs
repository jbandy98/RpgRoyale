using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour {

    public string roomName = "TestServer";
    public Room room;
    public GameObject player;
    public List<Transform> spawnpoints;
    public Camera playerCamera;

    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;

    public byte Version = 1;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;

    // Use this for initialization
    void Start () {
        PhotonNetwork.autoJoinLobby = false;
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
        // pick a random spawn point
        int randomSpawnPoint = Random.Range(0, spawnpoints.Count);
        GameObject playerSpawn = PhotonNetwork.Instantiate(player.name,new Vector3(0,0,0), Quaternion.identity,0);
        playerSpawn.GetComponent<PlayerManager>().gameData.location = new Vector3Int((int)spawnpoints[randomSpawnPoint].transform.position.x, (int)spawnpoints[randomSpawnPoint].transform.position.y, (int)spawnpoints[randomSpawnPoint].transform.position.z);
        playerCamera.transform.SetParent(playerSpawn.transform);
        playerCamera.transform.localPosition.Set(playerSpawn.transform.position.x+2.5f, playerSpawn.transform.position.y, playerSpawn.transform.position.z);
        playerSpawn.transform.position = spawnpoints[randomSpawnPoint].transform.position;
        PlayerManager playerManager = playerSpawn.GetComponent<PlayerManager>();
        playerManager.playerData.name = PlayerPrefs.GetString("user");
        playerManager.gameData.playerId = PlayerPrefs.GetString("user");
        playerManager.playerData.session = PlayerPrefs.GetString("sessionId");
        playerManager.gameData.sessionId = PlayerPrefs.GetString("sessionId");
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
}
