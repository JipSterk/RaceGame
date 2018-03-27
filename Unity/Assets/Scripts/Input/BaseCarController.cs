using RaceGame.Car;
using UnityEngine;

namespace RaceGame.Input
{
    public abstract class BaseCarController : MonoBehaviour
    {
        protected CarController CarController;

        protected virtual void Start()
        {
            CarController = GetComponent<CarController>();
        }

        protected abstract void FixedUpdate();
    }
}