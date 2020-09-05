namespace TreniniDotNet.SharedKernel.PhoneNumbers
{
    public static class StringConverterExtensions
    {
        public static PhoneNumber? ToPhoneNumberOpt(this string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            return PhoneNumber.Of(str);
        }
    }
}