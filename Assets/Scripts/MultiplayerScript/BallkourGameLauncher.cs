using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

namespace Com.Serzz0.Ballkour
{
    public class BallkourGameLauncher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        /// <summary>
        /// the maximum number of players per room. When a room is full, it
        /// can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
        #endregion

        #region Private Fields

        /// <summary>
        /// The client's version number. Users are separated from each other
        /// by gameVersion (which allows you to make a breaking changes).
        /// </summary>
        string gameVersion = "1";

        /// <summary>
        /// Keep track of the current process. Since connection is asynchronous
        /// and is based on several callbacks from Photon, we need to keep track
        /// of this to properly adjust the behavior when we receive call back by
        /// Photon. Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;

        #endregion

        #region Public Fields
        [Tooltip("The UI Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;
        public TMP_InputField nameInput;
        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during
        /// early initialization phase.
        /// </summary>
        void Awake()
        {
            // <summary>
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the
            // master client and all clients in the same room sync their
            // level automatically
            // </summary>
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during
        /// early initialization phase.
        /// </summary>
        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - If not yet connected, connect this application instance
        /// to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            // <summary>
            // we check if we are connected or not, we join if we are,
            // else we initiate the connection to the server.
            // </summary>
            if (string.IsNullOrEmpty(nameInput.text))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = nameInput.text;
            Debug.Log(nameInput.text);
            PlayerPrefs.SetString("PlayerName", nameInput.text);
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                // <summary>
                // #Critical, we must first and foremost connect to
                // Photon online Server.
                // </summary>
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // <summary>
                // keep track of the will join a room, because when
                // we come back from the game we will get a callback
                // that we are connected, so we need to know what to do
                // then
                // </summary>
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                // <summary>
                // #Critical, we must first and foremost connect to
                // Photon online Server.
                // </summary>
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            // <summary>
            // #Critical: The first we try to do is join a potential
            // existing room. If there is, good, else, we'll be called
            // back with OnJoinRandomFailed()
            // </summary>

            // <summary>
            // we don't want to do anything if we are not attempting to
            // join a room.
            // this case where isConnecting is false is typically when you
            // lost or quit the game, when this level is loaded, OnConnectedMaster
            // will be called, in that case we don't want to do anything.
            // </summary>
            if (isConnecting)
            {
                // <summary>
                // #Critical: The first we try to do is to join a potential 
                // existing room. If there is, good, else, we'll be called
                // back with the OnJoinRandomFailed()
                // </summary>
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {o}", cause);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            // <summary>
            // #Critical: we failed to join a random room, maybe none
            // exists or they are all full. No worries, we create a 
            // new room.
            // </summary>
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
        }

        public override void OnJoinedRoom() 
        {
            Debug.Log("PUN Basics Tutorial/launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            // <summary>
            // #Critical: We only load if we are the first player,
            // else we rely on PhotonNetwork.AutomaticallySyncScene`
            // to sync our instance scene.
            // </summary>
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");

                // <summary>
                // #Critical
                // Load the Room Level.
                // </summary>
                PhotonNetwork.LoadLevel("EasyLevel");
            }
        }

        #endregion
    }
}
