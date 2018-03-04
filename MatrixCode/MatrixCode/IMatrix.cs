using System.Collections.Generic;

namespace MatrixTask
{
    public interface IMatrix
    {
        void Delete(int i, int j);
        int GetDiagonalElementsSum();
        List<int> GetListOfMinimaInColumns();
        int[][] GetMatrix();
        void GetTwoColumnsSum(int j1, int j2);
        void Insert(int i, int j, int value);
        void Transpose();
    }
}
