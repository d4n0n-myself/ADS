﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix, IEnumerable<int>
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

            while (tempElement != null)
            {
                if (tempElement.Line == tempElement.Column ||
										tempElement.Line + tempElement.Column == _size - 1)
                    sum += tempElement.Value;
                tempElement = tempElement.NextItem;
            }

            return sum;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<int> GetEnumerator()
        {
            var element = _first;

            while (true)
            {
                yield return element.Value;
                if (element.NextItem != null)
                    element = element.NextItem;
                else
                    yield break;
            }
        }

        public List<int> GetListOfMinimaInColumns()
        {
            List<int> result = new List<int>();
            int line = 0;
            int currMin = Int16.MaxValue;
            var tempElement = _first;

            while (tempElement.NextItem != null)
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

        public int[][] GetMatrix()
        {
            int[][] originalMatrix = new int[_size][];
            var execElement = _first;

            for (int i = 0; i < _size; i++)
            {
                originalMatrix[i] = new int[_size];
                for (int j = 0; j < _size; j++)
                {
                    if (execElement.Column == j && execElement.Line == i)
                    {
                        originalMatrix[i][j] = execElement.Value;
                        execElement = execElement.NextItem;
                    }
                    else
                        originalMatrix[i][j] = 0;
                }
            }

            return originalMatrix;
        }

        public void GetTwoColumnsSum(int column1, int column2)
        {
            throw new NotImplementedException();
        }

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            CheckArgument(lineIndex, columnIndex);

            if (value == 0) 
            {
                Delete(lineIndex,columnIndex);
                return;
            }

            CheckForFirstElement(lineIndex, columnIndex, value);

            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

            if (tempElement.NextItem == null)
                tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
            else
                tempElement.NextItem.Value = value;
        }

        public void Transpose()
        {
            var tempElement = _first;

            while(tempElement.NextItem != null)
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

                var tempElement = _first;
                while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex)
                    if (tempElement.NextItem != null)
                        tempElement = tempElement.NextItem;

                if (tempElement.Line != lineIndex || tempElement.Column != columnIndex)
                    return 0;
                return tempElement.Value;
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

        private void InternalInsert(int lineIndex,int columnIndex,int value)
        {
            CheckForFirstElement(lineIndex,columnIndex,value);

            var tempElement = _first;

            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                if (tempElement.NextItem != null)
                    tempElement = tempElement.NextItem;

            tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value);
        }
    }
}