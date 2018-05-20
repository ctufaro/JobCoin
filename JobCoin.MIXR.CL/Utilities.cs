using System;
using System.Collections.Generic;
using System.Text;

namespace JobCoin.MIXR.CL
{
    public class Utilities
    {
        public static void Shuffle<T>(Random _random, T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + _random.Next(n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static List<string> GetDistributions(Random r, string totalAmount)
        {
            decimal tAmount = Convert.ToDecimal(totalAmount);
            int steps = r.Next(2, 7);
            decimal d = tAmount;
            decimal whole = Math.Floor(d);
            decimal rem = d % 1;
            List<int> dist = RandDist(r, Convert.ToInt32(whole), steps);
            int indexTack = r.Next(2, steps) - 1;
            List<string> finalAmounts = new List<string>();
            for (int i = 0; i < dist.Count; i++)
            {
                if (dist[i] == 0)
                    continue;
                if (i == indexTack)
                {
                    string amt = dist[i].ToString() + rem.ToString().Remove(0, 1);
                    finalAmounts.Add(amt);
                }
                else
                {
                    finalAmounts.Add(dist[i].ToString());
                }
            }
            return finalAmounts;
        }

        public static List<int> RandDist(Random r, int targetSum, int numberOfDraws)
        {
            List<int> load = new List<int>();

            //random numbers
            int sum = 0;
            for (int i = 0; i < numberOfDraws; i++)
            {
                int next = r.Next(targetSum) + 1;
                load.Add(next);
                sum += next;
            }

            //scale to the desired target sum
            double scale = 1d * targetSum / sum;
            sum = 0;
            for (int i = 0; i < numberOfDraws; i++)
            {
                load[i] = (int)(load[i] * scale);
                sum += load[i];
            }

            //take rounding issues into account
            while (sum++ < targetSum)
            {
                int i = r.Next(numberOfDraws);
                load[i] = load[i] + 1;
            }

            return load;
        }

    }
}
