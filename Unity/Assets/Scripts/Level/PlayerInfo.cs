using RaceGame.Car;
using UnityEngine;

namespace RaceGame.Level
{
    public struct PlayerInfo
    {
        public Vector3 LastWayPoint { get; set; }

        public string Name { get; }
        public CarController CarController { get; }

        public PlayerInfo(Vector3 lastWayPoint, string name, CarController carController)
        {
            LastWayPoint = lastWayPoint;
            Name = name;
            CarController = carController;
        }
    }
}