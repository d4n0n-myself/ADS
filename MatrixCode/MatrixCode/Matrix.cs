using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix
    {
        public int Size => _size;

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
                for (int j = 0; j < _size; j++)
                    InternalInsert(i, j, matrix[i][j]);
            }
        }

        public void Delete(int lineIndex, int columnIndex)
        {
            CheckArgument(lineIndex, columnIndex);
            if (lineIndex == _first.Line && columnIndex == _first.Column)
            {
                _first = _first.NextItem;
                return;
            }
						
            foreach (var element in _elements)
            {
                var next = element.NextItem;
                if (next != null && next.Column == columnIndex && next.Line == lineIndex)
                {
                    element.NextItem = next.NextItem;
                    return;
                }
            }
        }

        public bool Equals(int[][] other)
        {
            var matrix = GetMatrix();
            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                    if (matrix[i][j] != other[i][j])
                        return false;
            return true;
        }

        public int GetDiagonalElementsSum()
        {
            int sum = 0;
            foreach (var element in _elements)
                if (element.Line == element.Column ||
										element.Line + element.Column == _size - 1)
                    sum += element.Value;
            return sum;
        }

        public List<int> GetListOfMinimaOfColumns()
        {
            var columnsMin = new List<int>(_size);
            for (int i = 0; i < _size; i++)
                columnsMin.Add(int.MaxValue);

						foreach (var element in _elements)
                if (element.Value < columnsMin[element.Column])
                    columnsMin[element.Column] = element.Value;
            return columnsMin;
        }

        public int[][] GetMatrix()
        {
            var matrix = new int[_size][];
            for (int i = 0; i < _size; i++)
                matrix[i] = new int[_size];

            foreach (var item in _elements)
                matrix[item.Line][item.Column] = item.Value;
            return matrix;
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            if (value == 0)
            {
                Delete(lineIndex, columnIndex);
                return;
            }

            CheckArgument(lineIndex, columnIndex);
            InternalInsert(lineIndex, columnIndex, value);
        }

        public void MakeTwoColumnsSum(int column1, int column2)
        {
            foreach (var first in _elements)
            {
                if (first.Column != column1)
                    continue;
                foreach (var second in _elements)
                    if (second.Column == column2 && second.Line == first.Line)
                    {
                        first.Value += second.Value;
                        break;
                    }
						}
						DeleteZeroes();
        }

        public void Transpose()
        {
            foreach (var element in _elements)
            {
                var temp = element.Column;
                element.Column = element.Line;
                element.Line = temp;
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

        private class MatrixElement
        {
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

        private IEnumerable<MatrixElement> _elements
        {
            get
            {
                for (var element = _first; element != null; element = element.NextItem)
                    yield return element;
            }
        }

        private void CheckArgument(int lineIndex, int columnIndex)
        {
            if (lineIndex >= _size || lineIndex < 0)
                throw new ArgumentOutOfRangeException("lineIndex");
            if (columnIndex >= _size || columnIndex < 0)
                throw new ArgumentOutOfRangeException("columnIndex");
        }

        private void DeleteZeroes()
        {
            while (_first.Value == 0)
                _first = _first.NextItem;

            var current = _first;
            while (current != null)
            {
                var next = current.NextItem;
                if (next?.Value == 0)
                    current.NextItem = next.NextItem;
                else
                    current = current.NextItem;
						}
        }

        private void InternalInsert(int lineIndex, int columnIndex, int value)
        {
            MatrixElement previous = null;
            foreach (var element in _elements)
            {
                if (element.Column == columnIndex && element.Line == lineIndex)
                {
                    element.Value = value;
                    return;
                }
                previous = element;
            }

            var newElement = new MatrixElement(lineIndex, columnIndex, value);
						if (previous == null)
                _first = newElement;
						else
                previous.NextItem = newElement;
        }
    }
}