using RaceGame.Input;
using UnityEngine;

namespace RaceGame.Level
{
    public class Reviver : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var playerCarController = other.GetComponent<PlayerCarController>();

            if (!playerCarController) return;

            var playerInfo = GameManager.Instance.GetPlayerInfo(playerCarController.Player);

            playerCarController.transform.position = playerInfo.CheckPoint.transform.position + Vector3.up * 1f;
            playerCarController.transform.rotation = Quaternion.identity;
        }
    }
}