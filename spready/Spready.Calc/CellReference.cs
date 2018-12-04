using System;
using System.Text;

namespace Spready.Calc
{
    public class CellReference
    {
        private readonly CellCoordinates cellCoordinates;

        public CellReference(string referenceExpression)
        {
            this.cellCoordinates = ConvertReferenceToCoordinates(referenceExpression);
        }

        public CellReference(int row, int column)
        {
            this.cellCoordinates = new CellCoordinates(column, row);
        }

        public CellCoordinates GetCoordinates()
        {
            return this.cellCoordinates;
        }

        private CellCoordinates ConvertReferenceToCoordinates(string referenceExpression)
        {
            return ParseExpression(referenceExpression);
        }

        private CellCoordinates ParseExpression(string referenceExpression)
        {
            var columnAccumulator = new StringBuilder();
            int i = 0;
            for (; i < referenceExpression.Length; i++)
            {
                if (char.IsLetter(referenceExpression[i]))
                {
                    columnAccumulator.Append(referenceExpression[i]);
                }
                else
                {
                    break;
                }
            }
            var columnReference = columnAccumulator.ToString();

            var rowAccumulator = new StringBuilder();
            for (; i < referenceExpression.Length; i++)
            {
                if (char.IsDigit(referenceExpression[i]))
                {
                    rowAccumulator.Append(referenceExpression[i]);
                }
                else
                {
                    break;
                }
            }

            var rowReference = rowAccumulator.ToString();

            return new CellCoordinates(ConvertColumnNameToNumber(columnReference), Convert.ToInt32(rowReference));
        }

        /// <summary>
        /// Convert an Excel column name to equivalent number.
        /// </summary>
        /// <remarks>
        /// Solution taken from the accepted answer to the following question:
        /// http://stackoverflow.com/questions/667802/what-is-the-algorithm-to-convert-an-excel-column-letter-into-its-number
        /// </remarks>
        /// <param name="columnName">Column name.</param>
        /// <returns>Column number.</returns>
        private static int ConvertColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException(nameof(columnName));
            }
            var columnNameUpper = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnNameUpper.Length; i++)
            {
                sum *= 26;
                sum += (columnNameUpper[i] - 'A' + 1);
            }

            return sum;
        }
    }
}
