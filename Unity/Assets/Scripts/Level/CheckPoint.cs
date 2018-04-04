using RaceGame.Car;
using UnityEngine;

namespace RaceGame.Level
{
    public class CheckPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var carController = other.GetComponent<CarController>();

            if (!carController) return;

            GameManager.Instance.NextCheckPoint(carController.name, transform.position);
        }
    }
}