using RaceGame.Car;

namespace RaceGame.Level
{
    public struct PlayerInfo
    {
        public CheckPoint CheckPoint { get; set; }

        public Player Player { get; }
        public CarController CarController { get; }

        public PlayerInfo(CheckPoint checkPoint, Player player, CarController carController)
        {
            CheckPoint = checkPoint;
            Player = player;
            CarController = carController;
        }
    }
}