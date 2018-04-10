using System.Collections;
using System.Linq;
using Cinemachine;
using RaceGame.Input;
using UnityEngine;

namespace RaceGame.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private float _waitTime;
        [SerializeField] private Behaviour[] _behavioursToEnable;

        public static int PlayerToLayerMask(int playerId)
        {
            int[] playerIds = { 0, 1, 2, 3 };

            playerIds = playerIds.Where(x => x != playerId).ToArray();

            return ~(9 << LayerMask.NameToLayer($"Player {playerIds[0]} Camera") |
                     9 << LayerMask.NameToLayer($"Player {playerIds[1]} Camera") |
                     9 << LayerMask.NameToLayer($"Player {playerIds[2]} Camera"));
        }

        public IEnumerator Setup(CinemachineVirtualCamera cinemachineVirtualCamera, Camera playerCamera, int players,
            int playerId)
        {
            playerCamera.name = $"Player {playerId}'s Camera";
            playerCamera.cullingMask = PlayerToLayerMask(playerId);

            cinemachineVirtualCamera.name = $"Player {playerId}'s Cinemachine";
            cinemachineVirtualCamera.gameObject.layer = LayerMask.NameToLayer($"Player {playerId} Camera");

            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = transform;

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

            yield return new WaitForSeconds(_waitTime);

            foreach (var behaviour in _behavioursToEnable)
                behaviour.enabled = true;

            GetComponent<PlayerCarController>().Init(playerId);
        }
    }
}