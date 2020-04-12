using System;
using System.Net.Mail;

namespace TreniniDotNet.Common.Extensions
{
    public static class StringConverterExtensions
    {
        // Convert this nullable string to an MailAddress, 
        // if the value is null or not valid it will return a None
        public static MailAddress? ToMailAddressOpt(this string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            try
            {
                return new MailAddress(str);
            }
            catch
            {
                return null;
            }
        }

        // Convert this nullable string to an MailAddress, 
        // if the value is null or not valid it will return a None
        public static Uri? ToUriOpt(this string? str) =>
            str != null && Uri.TryCreate(str, UriKind.Absolute, out var uri) ?
                uri : null;
    }
}