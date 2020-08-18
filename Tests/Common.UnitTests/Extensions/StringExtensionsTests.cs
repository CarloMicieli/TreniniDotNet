using System;
using System.Net.Mail;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void String_ToMailAddressOpt_ShouldConvertToMailAddressWhenValid()
        {
            var mailString = "mail@mail.com";
            var mail = mailString.ToMailAddressOpt();

            mail.Should().Be(new MailAddress(mailString));
        }

        [Fact]
        public void String_ToMailAddressOpt_ShouldReturnNull_WhenStringIsNullOrEmpty()
        {
            string mailString = null;
            var mail = mailString.ToMailAddressOpt();

            mail.Should().BeNull();
        }

        [Fact]
        public void String_ToMailAddressOpt_ShouldReturnNull_WhenStringIsNotValidMailAddress()
        {
            string mailString = "invalid email";
            var mail = mailString.ToMailAddressOpt();

            mail.Should().BeNull();
        }

        [Fact]
        public void String_ToUriOpt_ShouldReturnUri_WhenStringIsValidUrl()
        {
            string url = "http://localhost";

            var uri = url.ToUriOpt();

            uri.Should().Be(new Uri(url));
        }

        [Fact]
        public void String_ToUriOpt_ShouldReturnNull_WhenStringIsInvalidUrl()
        {
            string url = "invalid url";

            var uri = url.ToUriOpt();

            uri.Should().BeNull();
        }

        [Fact]
        public void String_ToUriOpt_ShouldReturnNull_WhenStringIsNullOrEmpty()
        {
            string url = null;

            var uri = url.ToUriOpt();

            uri.Should().BeNull();
        }
    }
}
