using TreniniDotNet.Common;
using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    /// <summary>
    /// It represents a model railways rolling stock manufacturer.
    /// </summary>
    /// <remarks>
    public sealed class Brand : IBrand, IEquatable<Brand>
    {
        private readonly BrandId _id;
        private readonly Slug _slug;
        private readonly string _name;
        private readonly string? _companyName;
        private readonly Uri? _websiteUrl;
        private readonly MailAddress? _emailAddress;
        private readonly BrandKind _brandType;

        /// <summary>
        /// Creates a new <em>Brand</em>.
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="companyName">the full company name</param>
        /// <param name="websiteUrl">the website url</param>
        /// <param name="emailAddress">the email address</param>
        /// <param name="kind">the brand kind</param>
        public Brand(string name, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
            : this(BrandId.NewId(), name, Slug.Empty, companyName, websiteUrl, emailAddress, kind)
        {
        }

        /// <summary>
        /// Creates a new <em>Brand</em>.
        /// </summary>
        /// <param name="id">the brand id</param>
        /// <param name="name">the name</param>
        /// <param name="slug">the seo friendly name</param>
        /// <param name="companyName">the full company name</param>
        /// <param name="websiteUrl">the website url</param>
        /// <param name="emailAddress">the email address</param>
        /// <param name="kind">the brand kind</param>
        public Brand(BrandId id, string name, Slug slug, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            ValidateBrandName(name);

            _id = id;
            _slug = slug.OrNewIfEmpty(() => Slug.Of(name));
            _name = name;
            _websiteUrl = websiteUrl;
            _emailAddress = emailAddress;
            _companyName = companyName;
            _brandType = kind;
        }

        #region [ Properties ]
        public BrandId BrandId => _id;

        public Slug Slug => _slug;

        public string Name => _name;

        public Uri? WebsiteUrl => _websiteUrl;

        public MailAddress? EmailAddress => _emailAddress;

        public string? CompanyName => _companyName;

        public BrandKind Kind => _brandType;
        #endregion

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Brand that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Brand left, Brand right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Brand left, Brand right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(Brand other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(Brand left, Brand right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return left.Name == right.Name;
        }
        #endregion  

        #region [ Standard methods overrides ]
        public override int GetHashCode()
        {
            return _name.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return $"Brand({_name})";
        }
        #endregion

        public IBrandInfo ToBrandInfo()
        {
            return this;
        }

        private static void ValidateBrandName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(ExceptionMessages.InvalidBrandName);
            }
        }
    }
}
