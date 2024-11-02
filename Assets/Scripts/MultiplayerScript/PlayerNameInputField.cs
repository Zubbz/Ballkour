using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace Com.Serzz0.Ballkour
{
    /// <summary>
    /// Player name input field. Let the user input his name,
    /// will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants

        // <summary>
        // Store the PlayerPref Key to avoid typos
        // </summary>
        const string playerNamePrefkey = "PlayerName";

        #endregion

        #region MonoBehaviour CallBacks
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity
        /// during initialization phase.
        /// </summary>
        void Start()
        {
            string defaultName = string.Empty;
            TMP_InputField _InputField = this.GetComponent<TMP_InputField>();
            if (_InputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefkey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefkey);
                    _InputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the name of the player, and save it in the
        /// PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefkey, value);
        }

        #endregion
    }
}
