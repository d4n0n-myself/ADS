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
                    originalMatrix[i][j] = execElement.Value;
                    execElement = execElement.NextItem;
                }

            return originalMatrix;
        }

        public void Delete(int lineIndex, int columnIndex)
        {
            MatrixElement execElement = _first;

            for (int line = 0; line < lineIndex; line++)
                execElement = execElement.NextLineItem;
            for (int column = 0; column < columnIndex; column++)
                execElement = execElement.NextItem;

            execElement.Value = 0;
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

        public MatrixElement previousLineElement;

        public void Insert(int lineIndex, int columnIndex, int value)
        {
            if (lineIndex > _size || columnIndex > _size) throw new InvalidOperationException("Element out of matrix range");

            if (_first == null)
            {
                var startElement = new MatrixElement(lineIndex, columnIndex, value);
                previousLineElement = _first = startElement;
                return;
            }

            var tempElement = _first;
            while (tempElement.Line * _size + tempElement.Column < lineIndex * _size + columnIndex - 1)
                tempElement = tempElement.NextItem;

            if (tempElement.NextItem == null)
            { 
                tempElement.NextItem = new MatrixElement(lineIndex, columnIndex, value); 
                if (tempElement.Line > 0 && tempElement.Column == 0)
                {
                    if (tempElement.NextLineItem == null)
                        previousLineElement.NextLineItem = tempElement;
                    previousLineElement = tempElement;
                }
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

            var execOriginalElem = _first;
            var execExpectedElem = expected._first;
            var operationsCount = _size * _size;

            for (int i = 0; i < operationsCount; i++)
            {
                if (execExpectedElem.Value != execOriginalElem.Value)
                    return false;
                execOriginalElem = execOriginalElem.NextItem;
                execExpectedElem = execExpectedElem.NextItem;
            }

            return true;
        }

        public IEnumerator<MatrixElement> GetEnumerator()
        {
            int i = 0; int j = 0;
            var element = _first;
            while (i * _size + j < _size * _size - 1)
            {
                yield return element;
                element = element.NextItem;
            }
        }

        public Matrix this[int index]
        {
            get
            {
                return this[index];
            }
        }
    }
}