using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.BusinessComponent.Extensions
{
    public static class ArrayExtensions
    {
        public static bool CheckRow(this char[,] arr, int rowNum)
        {
            var length = arr.GetLength(1);
            var list = Enumerable.Range(0, length).Select(x => arr[rowNum, x]);
            var xCount = list.Count(x => x == 'X');
            var oCount = list.Count(x => x == 'O');

            return length == xCount || length == oCount;
        }

        public static bool CheckColumn(this char[,] arr, int colNum)
        {
            var length = arr.GetLength(0);
            var list = Enumerable.Range(0, length).Select(x => arr[x, colNum]);
            var xCount = list.Count(x => x == 'X');
            var oCount = list.Count(x => x == 'O');

            return length == xCount || length == oCount;
        }

        public static bool CheckDiagonals(this char[,] arr)
        {
            var length = arr.GetLength(0);
            List<char> d1 = new List<char>();
            List<char> d2 = new List<char>();

            for (int i = 0, j = length - 1; i < length; i++, j--)
            {
                d1.Add(arr[i, i]);
                d2.Add(arr[i, j]);
            }

            var xCount = d1.Count(x => x == 'X');
            var oCount = d2.Count(x => x == 'O');

            return length == xCount || length == oCount;
        }
    }
}
