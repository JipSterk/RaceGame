using System.Collections;
using System.Collections.Generic;
using RaceGame.Car;
using UnityEngine;

namespace RaceGame.Level
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance =>
            _instance ?? new GameObject(nameof(GameManager)).AddComponent<GameManager>();

        public List<string> Players { get; } = new List<string>();

        private readonly Dictionary<string, PlayerInfo> PlayerInfos = new Dictionary<string, PlayerInfo>();

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

        public void RegisterCarController(string playerName, CarController carController)
        {
            var playerInfo = new PlayerInfo(carController.transform.position, playerName, carController);

            if(PlayerInfos.ContainsKey(playerName)) return;
            PlayerInfos.Add(playerName, playerInfo);
        }

        public PlayerInfo GetPlayerInfo(string key) => PlayerInfos[key];

        public void NextCheckPoint(string key, Vector3 wayPoint)
        {
            var playerInfo = GetPlayerInfo(key);
            playerInfo.LastWayPoint = wayPoint;
        }
    }
}