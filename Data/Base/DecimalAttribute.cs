using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace JessicasAquariumMonitor.Data.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DecimalAttribute : ValidationAttribute
    {
        public DecimalAttribute(uint precision = 18, uint scale = 0)
        {
            if (precision < scale)
            {
                throw new ArgumentOutOfRangeException(nameof(scale), scale, @"Scale must not exceed precision");
            }

            Precision = precision;
            Scale = scale;

            ErrorMessage = $"Precision must not exceed {precision} and scale must not exceed {scale}";
        }

        public uint Precision { get; }
        public uint Scale { get; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is decimal))
            {
                throw new InvalidOperationException($"{nameof(DecimalAttribute)} is only applicable to decimal values");
            }

            var valueAsDecimal = (decimal) value;
            var valueAsString = valueAsDecimal.ToString(CultureInfo.InvariantCulture);
            var decimalParts = valueAsString.Split('.');
            var leftParts = decimalParts[0];
            var usedPrecision = leftParts.Length;

            if (decimalParts.Length <= 1)
            {
                return usedPrecision <= Precision;
            }

            var rightParts = decimalParts[1];
            var lengthRightOfDecimalPoint = rightParts.Length;
            var usedScale = lengthRightOfDecimalPoint;

            usedPrecision += lengthRightOfDecimalPoint;

            return usedPrecision <= Precision && usedScale <= Scale;
        }

        public override string FormatErrorMessage(string name) => $"For property: {name}, {ErrorMessage}";
    }
}