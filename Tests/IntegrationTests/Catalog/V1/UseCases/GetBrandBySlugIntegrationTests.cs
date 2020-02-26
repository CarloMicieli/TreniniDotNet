﻿using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetBrandBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetBrandBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsOk_WhenTheBrandExists()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands/acme");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetBrandBySlugResponse>(response);

            content.Should().NotBeNull();
            content.Id.Should().Be(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"));
            content.Name.Should().Be("ACME");
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("acme");
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsNotFound_WhenTheBrandDoesNotExist()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands/not-found");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        class GetBrandsListElementLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetBrandBySlugResponse
        {
            public GetBrandsListElementLinks _Links { set; get; }
            public Guid Id { set; get; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string MailAddress { get; set; }
            public string WebsiteUrl { get; set; }
            public string Kind { get; set; }
            public bool? Active { get; set; }
        }
    }
}