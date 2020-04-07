using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Net.Mail;

namespace TreniniDotNet.Common.Extensions
{
    public static class StringConverterExtensions
    {
        // Convert this nullable string to a Validation of Uri
        public static Validation<Error, Uri> ToUri(this string? str) =>
            str != null && Uri.TryCreate(str, UriKind.Absolute, out var uri) ?
                Success<Error, Uri>(uri) :
                Fail<Error, Uri>(Error.New("Invalid URI: The format of the URI could not be determined."));

        // Convert this nullable string to a Validation of MailAddress
        public static Validation<Error, MailAddress> ToMailAddress(this string? str) =>
            str != null && MailAddress_TryCreate(str, out var mailAddress) ?
                Success<Error, MailAddress>(mailAddress!) :
                Fail<Error, MailAddress>(Error.New("The specified string is not in the form required for an e-mail address."));

        // Convert this nullable string to an MailAddress, 
        // if the value is null or not valid it will return a None
        public static Option<MailAddress> ToMailAddressOpt(this string? str) =>
            str != null && MailAddress_TryCreate(str, out var mailAddress) ?
                Some(mailAddress!) : None;

        // Convert this nullable string to an MailAddress, 
        // if the value is null or not valid it will return a None
        public static Option<Uri> ToUriOpt(this string? str) =>
            str != null && Uri.TryCreate(str, UriKind.Absolute, out var uri) ?
                Some(uri) : None;

        private static bool MailAddress_TryCreate(string address, out MailAddress? result)
        {
            try
            {
                result = new MailAddress(address);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static Validation<Error, string> NonEmptyString(this string? str, string failMessage) =>
            !string.IsNullOrWhiteSpace(str) ? Success<Error, string>(str!) : Fail<Error, string>(Error.New(failMessage));

        public static Option<string> NonEmptyStringOpt(this string? str) =>
            !string.IsNullOrWhiteSpace(str) ? Some(str!) : None;

    }
}