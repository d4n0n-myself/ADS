﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixCode
{
    public class MatrixCode : MatrixCodeInterface
    {
        public MatrixElement First { get; private set; }
        public MatrixElement Last { get; private set; }
        public int Capacity { get; private set; }

        public MatrixCode(int[][] matrix)
        {
            if (matrix != null)
                Capacity = Math.Max(matrix.Length, matrix[0].Length);

            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    Insert(i, j, matrix[i][j]);
        }

        public void ColsSum(int j1, int j2)
        {
            throw new NotImplementedException();
        }

        public int[][] Decode()
        {
            int[][] originalMatrix = new int[Capacity][];
            var execElement = First;

            for (int i = 0; i < Capacity; i++)
                for (int j = 0; j < Capacity; j++)
                {
                    originalMatrix[i][j] = execElement.Value;
                    execElement = execElement.NextItem;
                }

            return originalMatrix;
        }

        public void Delete(int i, int j)
        {
            int line = 0;
            int column = 0;
            MatrixElement execElement = First;

            while (line != i)
                execElement = execElement.NextLineItem;
            while (column != j - 1)
                execElement = execElement.NextItem;

            execElement.Value = 0;
        }

        public int DiagSum()
        {
            throw new NotImplementedException();
        }

        public void Insert(int i, int j, int value)
        {
            if (i > Capacity || j > Capacity) throw new InvalidOperationException("Element out of matrix range");

            if (First == null)
            {
                var startElement = new MatrixElement(i, j, value);
                First = Last = startElement;
                return;
            }

            var tempElement = First;

            //while (tempElement.Line < i)
            //    tempElement = tempElement.NextLineItem;
            //while (tempElement.Column < j - 1)
                //tempElement = tempElement.NextItem;

            while (tempElement.Line * Capacity + tempElement.Column < i * Capacity + j - 1)
                tempElement = tempElement.NextItem;

            if (tempElement.NextItem == null)
                tempElement.NextItem = new MatrixElement(i, j, value);
            else
                tempElement.NextItem.Value = value;
        }

        public List<int> MinList()
        {
            throw new NotImplementedException();
        }

        public void Transp()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var expected = obj as MatrixCode;

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
    }
}