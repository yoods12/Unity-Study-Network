using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    readonly string version = "1.0";
    string userId = "Song";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;

        print(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected Master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Connected Lobby");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Joined Room Fail");
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("MyRoom", ro);
    }

    public override void OnCreatedRoom()
    {
        print("create room");
        print("room name : " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        print("Inroom : " + PhotonNetwork.InRoom);
        print("Player Count : " + PhotonNetwork.CurrentRoom.PlayerCount);

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            print(player.Value.NickName + " / " + player.Value.ActorNumber);
        }

        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);

        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);
    }

}
