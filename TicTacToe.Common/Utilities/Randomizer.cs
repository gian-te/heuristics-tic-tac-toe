using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Common.Utilities
{
    public static class Randomizer
    {
        public static Random Random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomizeNumber(int minInclusive = 1, int maxInclusive = 100)
        {
            maxInclusive += 1;
            // https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=netcore-3.1
            lock (syncLock)
            {
                return Random.Next(minInclusive, maxInclusive);
            }
        }
      
    }
}
