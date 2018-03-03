using System;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix
    {
        private MatrixElement _first;
        private readonly int _size;

        public Matrix(int[][] matrix)
        {
            if (matrix != null)
                _size = Math.Max(matrix.Length, matrix[0].Length);

            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    Insert(i, j, matrix[i][j]);
        }

        public void GetTwoColumnsSum(int column1, int column2)
        {
            throw new NotImplementedException();
        }

        public int[][] GetMatrix()
        {
            int[][] originalMatrix = new int[_size][];
            var execElement = _first;

            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    if (originalMatrix[i] == null)
                        originalMatrix[i] = new int[_size];
                    if (execElement.Column == j && execElement.Line == i)
                    {
                        originalMatrix[i][j] = execElement.Value;
                        execElement = execElement.NextItem;
                    }
                    else originalMatrix[i][j] = 0;
                }

            return originalMatrix;
        }

        public void Delete(int lineIndex, int columnIndex)
        {
            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex)
                tempElement = tempElement.NextItem;

            tempElement.Value = 0;
        }

        public int GetDiagonalElementsSum()
        {
            int sum = 0;

            foreach (var e in this)
            {
                if (e == null) break;
                if (e.Line == e.Column || e.Line + e.Column == _size - 1)
                    sum += e.Value;
            }

            return sum;
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            if (lineIndex > _size || columnIndex > _size) throw new InvalidOperationException("Element out of matrix range");
            if (value == 0) return;

            if (_first == null)
            {
                var startElement = new MatrixElement(lineIndex, columnIndex, value);
                _first = startElement;
                return;
            }

            var tempElement = _first;
            int line = 0;
            int column = 0;

            while (Math.Max(tempElement.Line, line) * _size + Math.Max(tempElement.Column, column) < lineIndex * _size + columnIndex - 1)
            {
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

                if (column < _size - 1)
                    column++;
                else
                {
                    column = 0;
                    line++;
                }
            }

            if (tempElement.NextItem == null)
            {
                tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
            }
            else
                tempElement.NextItem.Value = value;
        }

        public List<int> GetListOfMinimaInColumns()
        {
            throw new NotImplementedException();
        }

        public void Transpose()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var expected = obj as Matrix;

            var originalItem = _first;
            var expectedItem = expected._first;
            var operationsCount = _size * _size;

            while (expectedItem != null && originalItem != null)
            {
                if (expectedItem.Value != originalItem.Value)
                    return false;
                originalItem = originalItem.NextItem;
                expectedItem = expectedItem.NextItem;
            }

            return true;
        }

        public IEnumerator<MatrixElement> GetEnumerator()
        {
            var element = _first;

            while (true)
            {
                yield return element;
                if (element.NextItem != null)
                    element = element.NextItem;
                else yield break;
            }
        }

        public MatrixElement this[int lineIndex, int columnIndex]
        {
            get
            {
                var tempElement = _first;
                while (tempElement.Column < columnIndex && tempElement.Line < lineIndex && tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

                if (tempElement.Column == columnIndex && tempElement.Line == lineIndex)
                    return tempElement;
                throw new InvalidOperationException("Item's value == 0 or doesn't exist in matrix");
            }
        }
    }
}