using System.Collections;
using RaceGame.Input;
using UnityEngine;

namespace RaceGame.Level
{
    public class CheckPoint : MonoBehaviour
    {
        public int Index => _index;

        [SerializeField] private int _index;
        [SerializeField] private int _waitTime;

        private IEnumerator Start()
        {
            GameManager.Instance.RegisterCheckPoint(this);

            yield return new WaitForSeconds(_waitTime);
            GetComponent<Collider>().enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var carController = other.GetComponent<PlayerCarController>();

            if (!carController) return;

            GameManager.Instance.NextCheckPoint(carController.Player, this);
        }

        public static bool operator >(CheckPoint lhs, CheckPoint rhs) => lhs.Index > rhs.Index;
        public static bool operator <(CheckPoint lhs, CheckPoint rhs) => lhs.Index < rhs.Index;
    }
}