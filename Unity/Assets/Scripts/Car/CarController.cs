using System.Collections.Generic;
using UnityEngine;

namespace RaceGame.Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private List<AxleInfo> _axleInfos = new List<AxleInfo>();
        [SerializeField] private float _maxMotorTorque;
        [SerializeField] private float _maxSteeringAngle;

        public void ApplyLocalPositionToVisuals(WheelCollider wheelCollider)
        {
            if (wheelCollider.transform.childCount == 0)
                return;

            var visualWheel = wheelCollider.transform.GetChild(0);

            Vector3 position;
            Quaternion rotation;
            wheelCollider.GetWorldPose(out position, out rotation);

            visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }

        public void Steer(float vertical, float horizontal)
        {
            var motor = _maxMotorTorque * vertical;
            var steering = _maxSteeringAngle * horizontal;

            foreach (var axleInfo in _axleInfos)
            {
                if (axleInfo.Steering)
                {
                    axleInfo.LeftWheel.steerAngle = steering;
                    axleInfo.RightWheel.steerAngle = steering;
                }

                if (axleInfo.Motor)
                {
                    axleInfo.LeftWheel.motorTorque = motor;
                    axleInfo.RightWheel.motorTorque = motor;
                }

                ApplyLocalPositionToVisuals(axleInfo.LeftWheel);
                ApplyLocalPositionToVisuals(axleInfo.RightWheel);
            }
        }
    }
}