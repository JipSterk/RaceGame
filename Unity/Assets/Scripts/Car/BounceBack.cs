using System.Collections;
using RaceGame.Level;
using UnityEngine;

namespace RaceGame.Car
{
    public class BounceBack : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => StartCoroutine(TryBounceBack(other));

        private IEnumerator TryBounceBack(Collider other)
        {
            if(other.GetComponent<CheckPoint>()) yield break;

            var counter = 3;
            while (counter > 0)
            {
                yield return new WaitForSeconds(0.5f);

                if (!other) yield break;

                counter--;
            }

            var rotation = transform.root.rotation;
            transform.root.rotation = new Quaternion(rotation.x, rotation.y, 0f, 1.0f);
        }
    }
}