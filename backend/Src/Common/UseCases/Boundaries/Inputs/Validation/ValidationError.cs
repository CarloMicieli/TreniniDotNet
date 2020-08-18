using System;

namespace TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation
{
    public sealed class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; }
        public string ErrorMessage { get; }

        public override bool Equals(object? obj)
        {
            if (obj is ValidationError that)
            {
                return PropertyName.Equals(that.PropertyName) &&
                       ErrorMessage.Equals(that.ErrorMessage);
            }

            return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(PropertyName, ErrorMessage);

        public override string ToString() =>
            $"ValidationError({PropertyName}, {ErrorMessage})";
    }
}