using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RaceGame.Car;
using UnityEngine;

namespace RaceGame.Level
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance =>
            _instance ?? new GameObject(nameof(GameManager)).AddComponent<GameManager>();

        public List<string> Players { get; } = new List<string>();

        private List<CheckPoint> _checkPoints = new List<CheckPoint>();

        private readonly Dictionary<Player, PlayerInfo> _playerInfos = new Dictionary<Player, PlayerInfo>();

        private static GameManager _instance;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterPlayer(string player)
        {
            Players.Add(player);
        }

        public void RegisterCarController(CheckPoint checkPoint, Player player, CarController carController)
        {   
            var playerInfo = new PlayerInfo(checkPoint, player, carController);

            if(_playerInfos.ContainsKey(player)) return;
            _playerInfos.Add(player, playerInfo);
        }

        public PlayerInfo GetPlayerInfo(Player key) => _playerInfos[key];

        public CheckPoint GetCheckPoint(int index) => _checkPoints[index];

        public CheckPoint NextCheckPoint(Player key, CheckPoint checkPoint)
        {
            var playerInfo = GetPlayerInfo(key);
            
            if (playerInfo.CheckPoint < checkPoint)
                return checkPoint;

            playerInfo.CheckPoint = checkPoint;

            if (checkPoint.Index < _checkPoints.Count)
                return _checkPoints[checkPoint.Index + 1];

            playerInfo.Lap++;
            return _checkPoints[0];
        }
        
        public void RegisterCheckPoint(CheckPoint checkPoint)
        {
            _checkPoints.Add(checkPoint);
            _checkPoints = _checkPoints.OrderBy(x => x.Index).ToList();
        }
    }
}