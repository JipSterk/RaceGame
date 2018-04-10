using System;
using RaceGame.Car;

namespace RaceGame.Level
{
    public class PlayerInfo
    {
        public event Action<int> OnNextCheckPoint;

        public CheckPoint CheckPoint { get; set; }
        public int Lap
        {
            get { return _lap; }
            set
            {
                _lap = value;

                OnNextCheckPoint?.Invoke(_lap);
            }

        }

        public Player Player { get; }
        public CarController CarController { get; }

        private int _lap = 0;

        public PlayerInfo(CheckPoint checkPoint, Player player, CarController carController)
        {
            CheckPoint = checkPoint;
            Player = player;
            CarController = carController;
        }
    }
}