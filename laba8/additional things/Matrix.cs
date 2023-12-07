using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Matrix
    {
        float[,] matr;
        int colCount;
        int rowCount;

        public Matrix(int rows, int cols) { 
            rowCount = rows;
            colCount = cols;
            matr = new float[rowCount, colCount];
        }
        public Matrix Fill(params float[] elems)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    matr[i, j] = (float)Math.Round(elems[i * colCount + j], 5);
                }
            }
            return this;
        }

        public Matrix FillAffine(params float[] elems)
        {
            return Fill(elems[0], elems[1], 0, elems[2], elems[3], 0, elems[4], elems[5], 1);
        }

        public float this[int x, int y]
        {
            get
            {
                return matr[x, y];
            }
            set
            {
                matr[x, y] = value;
            }
        }

        public static Matrix operator *(Matrix matr, float value)
        {
            var res = new Matrix(matr.rowCount, matr.colCount);
            for (int i = 0; i < matr.rowCount; i++)
            {
                for (int j = 0; j < matr.colCount; j++)
                {
                    res[i, j] = matr[i, j] * value;
                }
            }
            return res;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.colCount != matrix2.rowCount)
            {
                throw new Exception("Invalid opertation");
            }
            var res = new Matrix(matrix1.rowCount, matrix2.colCount);
            for (int i = 0; i < res.rowCount; i++)
            {
                for (int j = 0; j < res.colCount; j++)
                {
                    for (var k = 0; k < matrix1.colCount; k++)
                    {
                        res[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return res;
        }
    }
}
