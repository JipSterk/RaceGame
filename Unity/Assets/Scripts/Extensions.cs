using System.Linq;
using UnityEngine;

namespace RaceGame
{
    public static class Extensions
    {
        public static int PlayerToLayerMask(this int playerId)
        {
            int[] array = {0, 1, 2, 3};

            array = array.Where(x => x != playerId).ToArray();

            return ~(9 << LayerMask.NameToLayer($"Player {array[0]} Camera") |
                     9 << LayerMask.NameToLayer($"Player {array[1]} Camera") |
                     9 << LayerMask.NameToLayer($"Player {array[2]} Camera"));
        }
    }
}