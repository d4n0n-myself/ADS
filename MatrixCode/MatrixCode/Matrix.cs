using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix
    {
        public Matrix(int size)
        {
            if (size <= 0)
                throw new ArgumentException("size");
            _size = size;
        }

        public Matrix(int[][] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");
            if (matrix.Length == 0)
                throw new ArgumentException("matrix");

            _size = matrix.Length;

            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Length != _size)
                    throw new ArgumentException("matrix");
                for (int j = 0; j < matrix[i].Length; j++)
                    InternalInsert(i, j, matrix[i][j]);
            }
        }

        public void Delete(int lineIndex, int columnIndex)
        {
            if (lineIndex == _first.Line && columnIndex == _first.Column)
            {
                _first = _first.NextItem;
                return;
            }

            var tempElement = _first;

            while (tempElement.NextItem != null &&
                   (tempElement.NextItem.Column != columnIndex || tempElement.NextItem.Line != lineIndex))
                tempElement = tempElement.NextItem;

            if (tempElement.NextItem != null)
            {
                MatrixElement newNextElement = null;
                if (tempElement.NextItem.NextItem != null)
                    newNextElement = tempElement.NextItem.NextItem;
                tempElement.NextItem = newNextElement;
            }
        }

        public bool Equals(int[][] expected)
        {
            var answer = GetMatrix();

            for (int k = 0; k < answer.Length; k++)
                for (int l = 0; l < answer[0].Length; l++)
                    if (answer[k][l] != expected[k][l])
                        return false;
            return true;
        }

        public int GetDiagonalElementsSum()
        {
            int sum = 0;
            var tempElement = _first;

            while (tempElement != null)
            {
                if (tempElement.Line == tempElement.Column ||
                                        tempElement.Line + tempElement.Column == _size - 1)
                    sum += tempElement.Value;
                tempElement = tempElement.NextItem;
            }

            return sum;
        }

        public List<int> GetListOfMinimalInColumns()
        {
            List<int> result = new List<int>();
            int[] columnsMin = new int[_size];
            for (int i = 0; i < _size; i++)
                columnsMin[i] = Int32.MaxValue;
            var tempElement = _first;

            while (tempElement != null)
            {
                if (tempElement.Value < columnsMin[tempElement.Column])
                    columnsMin[tempElement.Column] = tempElement.Value;
                tempElement = tempElement.NextItem;
            }

            result.AddRange(columnsMin);

            return result;
        }

        public int[][] GetMatrix()
        {
            int[][] originalMatrix = new int[_size][];
            var execElement = _first;

            for (int i = 0; i < _size; i++)
                originalMatrix[i] = new int[_size];

            foreach (var item in _elements)
                originalMatrix[item.Line][item.Column] = item.Value;

            return originalMatrix;
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            CheckArgument(lineIndex, columnIndex);

            if (value == 0)
            {
                Delete(lineIndex, columnIndex);
                return;
            }

            CheckForFirstElement(lineIndex, columnIndex, value);

            MatrixElement previousElement = null;
            var tempElement = _first;

            foreach (var element in _elements)
            {
                if (element.Column == columnIndex && element.Line == lineIndex)
                {
                    element.Value = value;
                    return;
                }
                previousElement = element;
            }


            previousElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
        }

        public void MakeTwoColumnsSum(int column1, int column2)
        {
            var tempElement = _first;

            while (tempElement != null)
            {
                if (tempElement.Column != column1)
                {
                    tempElement = tempElement.NextItem;
                    continue;
                }

                var newTempElement = _first;
                while (newTempElement != null)
                {
                    if (newTempElement.Column == column2 && newTempElement.Line == tempElement.Line)
                    {
                        tempElement.Value += newTempElement.Value;
                        tempElement = tempElement.NextItem;
                        break;
                    }
                    newTempElement = newTempElement.NextItem;
                }

            }
        }

        public void Transpose()
        {
            var tempElement = _first;

            while (tempElement != null)
            {
                int exec = tempElement.Column;
                tempElement.Column = tempElement.Line;
                tempElement.Line = exec;

                tempElement = tempElement.NextItem;
            }
        }

        public int this[int lineIndex, int columnIndex]
        {
            get
            {
                CheckArgument(lineIndex, columnIndex);

                foreach (var element in _elements)
                    if (element.Line == lineIndex && element.Column == columnIndex)
                        return element.Value;

                return 0;
            }
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
            public int Line { get; set; }
            public int Column { get; set; }
            public int Value { get; set; }
        }

        private MatrixElement _first;
        private readonly int _size;
        public int Size { get { return _size; } }

        private void CheckArgument(int lineIndex, int columnIndex)
        {
            if (lineIndex >= _size || lineIndex < 0)
                throw new ArgumentOutOfRangeException("lineIndex");
            if (columnIndex >= _size || columnIndex < 0)
                throw new ArgumentOutOfRangeException("columnIndex");
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

        private void InternalInsert(int lineIndex, int columnIndex, int value)
        {
            CheckForFirstElement(lineIndex, columnIndex, value);

            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

            tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
        }

        private IEnumerable<MatrixElement> _elements
        {
            get
            {
                for (var element = _first; element != null; element = element.NextItem)
                    yield return element;
            }
        }
    }
}