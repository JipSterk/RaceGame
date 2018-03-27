using System;
using UnityEngine;

namespace RaceGame.Car
{
    [Serializable]
    public class AxleInfo
    {
        public WheelCollider LeftWheel;
        public WheelCollider RightWheel;
        public bool Motor;
        public bool Steering;
    }
}