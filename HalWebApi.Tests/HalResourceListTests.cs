﻿using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using ApprovalTests;
using ApprovalTests.Reporters;
using HalWebApi.Tests.Linkers;
using HalWebApi.Tests.Representations;
using Xunit;

namespace HalWebApi.Tests
{
    public class HalResourceListTests
    {
        readonly ResourceLinker resourceLinker;
        readonly ResourceList<OrganisationRepresentation> resource;

        public HalResourceListTests()
        {
            resourceLinker = new ResourceLinker();
            resourceLinker.AddLinker(new OrganisationListLinker());
            resourceLinker.AddLinker(new OrganisationLinker());
            resource = new ResourceList<OrganisationRepresentation>(
                new List<OrganisationRepresentation>
                       {
                           new OrganisationRepresentation(1, "Org1"),
                           new OrganisationRepresentation(2, "Org2")
                       });
            resourceLinker.CreateLinks(resource);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void organisation_list_get_xml_test()
        {
            // arrange
            var mediaFormatter = new XmlHalMediaTypeFormatter();
            var contentHeaders = new StringContent(string.Empty).Headers;
            var type = resource.GetType();

            // act
            using (var stream = new MemoryStream())
            {
                mediaFormatter.WriteToStream(type, resource, stream, contentHeaders);
                stream.Seek(0, SeekOrigin.Begin);
                var serialisedResult = new StreamReader(stream).ReadToEnd();

                // assert
                Approvals.Verify(serialisedResult);
            }
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void organisation_list_get_json_test()
        {
            // arrange
            var mediaFormatter = new JsonHalMediaTypeFormatter { Indent = true };
            var contentHeaders = new StringContent(string.Empty).Headers;
            var type = resource.GetType();

            // act
            using (var stream = new MemoryStream())
            {
                mediaFormatter.WriteToStreamAsync(type, resource, stream, contentHeaders, null);
                stream.Seek(0, SeekOrigin.Begin);
                var serialisedResult = new StreamReader(stream).ReadToEnd();

                // assert
                Approvals.Verify(serialisedResult);
            }
        }
    }
}