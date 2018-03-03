using System.Collections.Generic;

namespace MatrixTask
{
    public interface IMatrix
    {
        int[][] DecodeToOriginalMatrix();
        void InsertNewElement(int i, int j, int value);
        void DeleteElementFromMatrix(int i, int j);
        List<int> GetColumnMinList();
        int GetDiagonalElementsSum();
        void TransposeMatrix();
        void GetTwoColumnsSum(int j1, int j2);
    }
}
