using System.Collections.Generic;

namespace MatrixTask
{
    public interface IMatrix
    {
        int[][] GetMatrix();
        void Insert(int i, int j, int value);
        void Delete(int i, int j);
        List<int> GetListOfMinimaInColumns();
        int GetDiagonalElementsSum();
        void Transpose();
        void GetTwoColumnsSum(int j1, int j2);
    }
}
