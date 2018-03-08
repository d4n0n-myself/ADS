using System.Collections.Generic;

namespace MatrixTask
{
    public interface IMatrix
    {
        void Delete(int i, int j);
        int GetDiagonalElementsSum();
        List<int> GetListOfMinimaOfColumns();
        int[][] GetMatrix();
        void MakeTwoColumnsSum(int j1, int j2);
        void Insert(int i, int j, int value);
        void Transpose();
    }
}