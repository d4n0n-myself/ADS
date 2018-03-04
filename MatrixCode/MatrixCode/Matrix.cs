using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix, IEnumerable
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

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                tempElement = tempElement.NextItem;

            tempElement.Value = 0;
        }

        public int GetDiagonalElementsSum()
        {
            int sum = 0;
            var tempElement = _first;

            while(tempElement!=null)
            {
                if (tempElement.Line == tempElement.Column || tempElement.Line + tempElement.Column == _size - 1)
                    sum += tempElement.Value;
                tempElement = tempElement.NextItem;
            }

            return sum;
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            if (lineIndex > _size || columnIndex > _size) throw new InvalidOperationException("Element out of matrix range");
            if (value == 0) 
            {
                //Delete(lineIndex,columnIndex);
                return;
            }

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
            List<int> result = new List<int>();
            int line = 0;
            int currMin = Int16.MaxValue;
            var tempElement = _first;

            while(tempElement.NextItem!=null)
            {
                if (line < tempElement.Line)
                {
                    result.Add(currMin);
                    currMin = Int16.MaxValue;
                }

                if (tempElement.Value < currMin)
                    currMin = tempElement.Value;

                tempElement = tempElement.NextItem;
            }

            return result;
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

        private sealed class MatrixElement
        {
            public MatrixElement() : this(0, 0) { }

            public MatrixElement(int line, int column)
            {
                Line = line;
                Column = column;
            }

            public MatrixElement(int line, int column, int value)
            {
                Line = line;
                Column = column;
                Value = value;
            }

            public MatrixElement NextItem { get; set; }
            public int Line { get; private set; }
            public int Column { get; private set; }
            public int Value { get; set; }
        }

        public IEnumerator GetEnumerator()
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int this[int lineIndex, int columnIndex]
        {
            get
            {
                if (lineIndex > _size || columnIndex > _size)
                    throw new ArgumentOutOfRangeException();

                var tempElement = _first;
                while (tempElement.Column < columnIndex && tempElement.Line < lineIndex || tempElement.NextItem == null)
                    if (tempElement.NextItem != null)
                        tempElement = tempElement.NextItem;

                return tempElement.Value;
            }
        }
    }
}