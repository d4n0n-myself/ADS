﻿using System;
using System.Collections.Generic;

namespace MatrixTask
{
    public class Matrix : IMatrix
    {
        MatrixElement First { get; set; }
        public int Capacity { get; private set; }

        public Matrix(int[][] matrix)
        {
            if (matrix != null)
                Capacity = Math.Max(matrix.Length, matrix[0].Length);

            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    InsertNewElement(i, j, matrix[i][j]);
        }

        public void GetTwoColumnsSum(int j1, int j2)
        {
            throw new NotImplementedException();
        }

        public int[][] DecodeToOriginalMatrix()
        {
            int[][] originalMatrix = new int[Capacity][];
            var execElement = First;

            for (int i = 0; i < Capacity; i++)
                for (int j = 0; j < Capacity; j++)
                {
                    if (originalMatrix[i] == null)
                        originalMatrix[i] = new int[Capacity];
                    originalMatrix[i][j] = execElement.Value;
                    execElement = execElement.NextItem;
                }

            return originalMatrix;
        }

        public void DeleteElementFromMatrix(int i, int j)
        {
            MatrixElement execElement = First;

            for (int line = 0; line < i; line++)
                execElement = execElement.NextLineItem;
            for (int column = 0; column < j; column++)
                execElement = execElement.NextItem;

            execElement.Value = 0;
        }

        public int GetDiagonalElementsSum()
        {
            int sum = 0;

            foreach (var e in this)
            {
                if (e == null) break;
                if (e.Line == e.Column || e.Line + e.Column == Capacity - 1)
                    sum += e.Value;
            }

            return sum;
        }

        public MatrixElement previousLineElement;

        public void InsertNewElement(int i, int j, int value)
        {
            if (i > Capacity || j > Capacity) throw new InvalidOperationException("Element out of matrix range");

            if (First == null)
            {
                var startElement = new MatrixElement(i, j, value);
                previousLineElement = First = startElement;
                return;
            }

            var tempElement = First;
            while (tempElement.Line * Capacity + tempElement.Column < i * Capacity + j - 1)
                tempElement = tempElement.NextItem;

            if (tempElement.NextItem == null)
            { 
                tempElement.NextItem = new MatrixElement(i, j, value); 
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

        public List<int> GetColumnMinList()
        {
            throw new NotImplementedException();
        }

        public void TransposeMatrix()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var expected = obj as Matrix;

            var execOriginalElem = First;
            var execExpectedElem = expected.First;
            var operationsCount = Capacity * Capacity;

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
            var element = First;
            while (i * Capacity + j < Capacity * Capacity - 1)
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