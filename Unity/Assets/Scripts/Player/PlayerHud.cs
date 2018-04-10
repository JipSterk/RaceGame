using System.Collections;
using RaceGame.Level;
using TMPro;
using UnityEngine;

namespace RaceGame.Player
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private float _planeDistance = 20;
        [SerializeField] private float _waitTime = 3f;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _playerLap;

        public IEnumerator Init(Camera playerCamera, int playerId)
        {
            _canvas.name = $"Player {playerId}'s HUD";
            _canvas.worldCamera = playerCamera;
            _canvas.planeDistance = _planeDistance;

            _playerLap.text = "Lap: 0";

            yield return new WaitForSeconds(_waitTime);

            var playerInfo = GameManager.Instance.GetPlayerInfo((Level.Player) playerId);

            playerInfo.OnNextCheckPoint += lap => _playerLap.text = $"Lap: {lap}";
        }
    }
}