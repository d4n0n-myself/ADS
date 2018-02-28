using System.Collections.Generic;

namespace MatrixCode
{
    public interface MatrixCodeInterface
    {
        int[][] Decode();
        void Insert(int i, int j, int value);
        void Delete(int i, int j);
        List<int> MinList();
        int DiagSum();
        void Transp();
        void ColsSum(int j1, int j2);
    }
}
