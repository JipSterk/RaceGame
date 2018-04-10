using System;
using UnityEngine;

namespace RaceGame.Car
{
    [Serializable]
    public class AxleInfo
    {
        public WheelCollider LeftWheel => _leftWheel;
        public WheelCollider RightWheel => _rightWheel;
        public bool Motor => _motor;
        public bool Steering => _steering;

        [SerializeField] private WheelCollider _leftWheel;
        [SerializeField] private WheelCollider _rightWheel;
        [SerializeField] private bool _motor;
        [SerializeField] private bool _steering;
    }
}