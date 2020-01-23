using System;
using System.Collections.Generic;
using System.Linq;

namespace RDI.Test.API
{
    public class Transformations
    {
        #region ABSOLUTE_DIFFERENCE
        /// <summary>
        /// Calculates the absolute difference between any two of the chosen integers less than or equal the specified value.
        /// <para>This method receives an int[].</para>
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        protected static string AbsoluteDifference(int[] originalArray, int difference)
        {
            List<int> array = new List<int>();
            try
            {
                array = originalArray.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AbsoluteDifference(array, difference);
        }

        /// <summary>
        /// Calculates the absolute difference between any two of the chosen integers less than or equal the specified value.
        /// <para>This method receives a string.</para>
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static string AbsoluteDifference(string originalArray, int difference)
        {
            int n;
            List<int> array = new List<int>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                if (!int.TryParse(originalArray.Substring(i, 1), out n))
                {
                    throw new Exception(string.Format("'{0}' is not a valid integer.", originalArray.Substring(i, 1)));
                }
                array.Add(n);
            }

            return AbsoluteDifference(array, difference);
        }

        /// <summary>
        /// Calculates the absolute difference between any two of the chosen integers less than or equal the specified value.
        /// <para>This method receives a List of int.</para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static string AbsoluteDifference(List<int> array, int difference)
        {
            if (array.Count == 0)
                throw new Exception("The array is empty.");

            int min = array.Min(x => x);
            int max = min + difference;

            array.RemoveAll(x => x > max);

            string saida = "";
            foreach (int i in array)
                saida += i.ToString();

            return saida;
        }
        #endregion ABSOLUTE_DIFFERENCE

        #region ROTATE
        /// <summary>
        /// Rotates an array N times.
        /// <para>This method receives an int[].</para>
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="numRotations"></param>
        /// <returns></returns>
        public static string Rotate(int[] originalArray, int numRotations)
        {
            List<int> array = new List<int>();
            try
            {
                array = originalArray.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RotateNumTimes(array, numRotations);
        }

        /// <summary>
        /// Rotates an array N times.
        /// <para>This method receives a string.</para>
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="numRotations"></param>
        /// <returns></returns>
        public static string Rotate(string originalArray, int numRotations)
        {
            int n;
            List<int> array = new List<int>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                if (!int.TryParse(originalArray.Substring(i, 1), out n))
                {
                    throw new Exception(string.Format("'{0}' is not a valid integer.", originalArray.Substring(i, 1)));
                }
                array.Add(n);
            }

            return RotateNumTimes(array, numRotations);
        }

        /// <summary>
        /// Rotates an array N times.
        /// <para>This method receives a List of int.</para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="numRotations"></param>
        /// <returns></returns>
        protected static string RotateNumTimes(List<int> array, int numRotations)
        {
            for (int i = 0; i < numRotations; i++)
            {
                array = RotateOnce(array);
            }

            string saida = "";
            foreach (int i in array)
                saida += i.ToString();

            return saida;
        }

        // 3,4,5 => 5,3,4 => 4,5,3
        /// <summary>
        /// Rotate an array once.
        /// <para>This method receives a List of int.</para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="numRotations"></param>
        /// <returns></returns>
        protected static List<int> RotateOnce(List<int> array)
        {
            if (array.Count == 0)
                throw new Exception("The array is empty.");

            int n = array.Last();
            array.RemoveAt(array.Count - 1);
            array.Insert(0, n);

            return array;
        }
        #endregion ROTATE
    }
}
