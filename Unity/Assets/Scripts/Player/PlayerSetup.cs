using System.Collections;
using Cinemachine;
using RaceGame.Input;
using UnityEngine;

namespace RaceGame.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private float _waitTime;
        [SerializeField] private Behaviour[] _behavioursToEnable;

        public IEnumerator Setup(CinemachineVirtualCamera cinemachineVirtualCamera, Camera playerCamera, int players,
            int playerId)
        {
            playerCamera.name = $"Player {playerId}'s Camera";
            playerCamera.cullingMask = playerId.PlayerToLayerMask();

            cinemachineVirtualCamera.name = $"Player {playerId}'s Cinemachine";
            cinemachineVirtualCamera.gameObject.layer = LayerMask.NameToLayer($"Player {playerId} Camera");

            switch (players)
            {
                case 2:
                    playerCamera.rect = playerId == 0 ? new Rect(0f, 0f, .5f, 1f) : new Rect(0.5f, 0f, 1f, 1f);
                    break;
                case 3:
                    switch (playerId)
                    {
                        case 0:
                            playerCamera.rect = new Rect(0f, 0.5f, 0.5f, 1f);
                            break;
                        case 1:
                            playerCamera.rect = new Rect(0.5f, 0.5f, 1f, 1f);
                            break;
                        case 2:
                            playerCamera.rect = new Rect(0f, 0f, 0.5f, 0.5f);
                            break;
                    }

                    break;
                case 4:
                    switch (playerId)
                    {
                        case 0:
                            playerCamera.rect = new Rect(0f, 0.5f, 0.5f, 1f);
                            break;
                        case 1:
                            playerCamera.rect = new Rect(0.5f, 0.5f, 1f, 1f);
                            break;
                        case 2:
                            playerCamera.rect = new Rect(0f, 0f, 0.5f, 0.5f);
                            break;
                        case 3:
                            playerCamera.rect = new Rect(0.5f, 0f, 1f, 0.5f);
                            break;
                    }

                    break;
            }

            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = transform;

            yield return new WaitForSeconds(_waitTime);

            foreach (var behaviour in _behavioursToEnable)
                behaviour.enabled = true;

            GetComponent<PlayerCarController>().Init(playerId);
        }
    }
}