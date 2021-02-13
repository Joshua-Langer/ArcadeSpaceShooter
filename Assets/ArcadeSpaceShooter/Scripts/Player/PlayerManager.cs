using System.Collections;
using System.Collections.Generic;
using ArcadeShooter.Managers;
using UnityEngine;

namespace ArcadeShooter.Player{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] Camera sceneCamera;
        PlayerMover playerMover;
        PlayerInput playerInput;
        PlayerWeapon playerWeapon;

        public static string playerTagName = "Player";


        void Awake()
        {
            playerMover = GetComponent<PlayerMover>();
            playerInput = GetComponent<PlayerInput>();
            playerWeapon = GetComponent<PlayerWeapon>();
            EnablePlayer(true);
        }

        void Update()
        {
            if(GameManager.IsGameOver())
            {
                return;
            }
            playerWeapon.IsFireButtonDown = playerInput.IsFiring;
        }

        void FixedUpdate()
        {
            if(GameManager.IsGameOver())
            {
                EnablePlayer(false);
                return;
            }
            Vector3 input = playerInput.GetCameraSpaceInputDirection(sceneCamera);
            playerMover.MovePlayer(input);
        }

        public static void EnablePlayer(bool state)
        {
            GameObject[] allPlayerObjects = GameObject.FindGameObjectsWithTag(playerTagName);
            foreach(GameObject go in allPlayerObjects)
            {
                go.SetActive(state);
            }
        }
    }
}
