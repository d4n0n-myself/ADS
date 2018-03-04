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
                    InternalInsert(i, j, matrix[i][j]);
        }

        private void CheckForFirstElement(int lineIndex, int columnIndex, int value)
        {
            if (_first == null)
            {
                var startElement = new MatrixElement(lineIndex, columnIndex, value);
                _first = startElement;
                return;
            }
        }

        private void InternalInsert(int lineIndex,int columnIndex,int value)
        {
            CheckForFirstElement(lineIndex,columnIndex,value);

            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

            tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
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
            if (lineIndex == 0 && columnIndex == 0)
            {
                if (_first.NextItem != null)
                    _first = _first.NextItem;
                return;
            }
            
            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                tempElement = tempElement.NextItem;

            if (tempElement.NextItem != null)
            {
                MatrixElement newNextElement;
                if (tempElement.NextItem.NextItem != null)
                {
                    newNextElement = tempElement.NextItem.NextItem;
                    tempElement.NextItem = newNextElement;
                }
            }
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

        private void CheckArgument(int lineIndex, int columnIndex)
        {
            if (lineIndex > _size || lineIndex < 0) throw new ArgumentOutOfRangeException();
            if (columnIndex > _size || columnIndex < 0) throw new ArgumentOutOfRangeException();
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            CheckArgument(lineIndex,columnIndex);

            if (value == 0) 
            {
                Delete(lineIndex,columnIndex);
                return;
            }

            CheckForFirstElement(lineIndex,columnIndex,value);

            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

            if (tempElement.NextItem == null)
                tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
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
                CheckArgument(lineIndex, columnIndex);

                var tempElement = _first;
                while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex)
                    if (tempElement.NextItem != null)
                        tempElement = tempElement.NextItem;

                if (tempElement.Line != lineIndex || tempElement.Column != columnIndex)
                    throw new InvalidOperationException();
                return tempElement.Value;
            }
        }
    }
}