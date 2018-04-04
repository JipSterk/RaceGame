using Cinemachine;
using RaceGame.Level;
using UnityEngine;

namespace RaceGame.Player
{
    public class GameSetup : MonoBehaviour
    {
        [SerializeField] private PlayerSetup _playerSetup;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private Transform[] _startingPositions = new Transform[4];

        private void Start()
        {
            for (var i = 0; i < GameManager.Instance.Players.Count; i++)
            {
                var playerSetup = Instantiate(_playerSetup, _startingPositions[i].position, Quaternion.identity);
                var playerCamera = Instantiate(_playerCamera);
                var cinemachineVirtualCamera = Instantiate(_cinemachineVirtualCamera);
                StartCoroutine(playerSetup.Setup(cinemachineVirtualCamera, playerCamera, GameManager.Instance.Players.Count, i));
            }
        }
    }
}