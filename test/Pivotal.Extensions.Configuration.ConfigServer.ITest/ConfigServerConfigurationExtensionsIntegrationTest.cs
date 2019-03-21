﻿//
// Copyright 2015 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//


using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

namespace Pivotal.Extensions.Configuration.ConfigServer.ITest
{
    //
    // NOTE: Some of the tests assume a running Spring Cloud Config Server is started
    //       with repository data for application: foo, profile: development
    //
    //       The easiest way to get that to happen is clone the spring-cloud-config
    //       repo and run the config-server.
    //          eg. git clone https://github.com/spring-cloud/spring-cloud-config.git
    //              cd spring-cloud-config\spring-cloud-config-server
    //              mvn spring-boot:run
    //

    public class ConfigServerConfigurationExtensionsIntegrationTest
    {
        public ConfigServerConfigurationExtensionsIntegrationTest()
        {
        }

        [Fact]
        public async void SpringCloudConfigServer_ConfiguredViaCloudfoundryEnv_ReturnsExpectedDefaultData_AsInjectedOptions()
        {
            // Arrange
            var VCAP_APPLICATION = @" 
{

    'application_id': 'fa05c1a9-0fc1-4fbd-bae1-139850dec7a3',
    'application_name': 'foo',
    'application_uris': [
    'foo.10.244.0.34.xip.io'
    ],
    'application_version': 'fb8fbcc6-8d58-479e-bcc7-3b4ce5a7f0ca',
    'limits': {
    'disk': 1024,
    'fds': 16384,
    'mem': 256
    },
    'name': 'foo',
    'space_id': '06450c72-4669-4dc6-8096-45f9777db68a',
    'space_name': 'my-space',
    'uris': [
    'foo.10.244.0.34.xip.io',
    'foo.10.244.0.34.xip.io'
    ],
    'users': null,
    'version': 'fb8fbcc6-8d58-479e-bcc7-3b4ce5a7f0ca'
}";


            var VCAP_SERVICES = @"
{
    'p-config-server': [
    {
    'credentials': {
        'access_token_uri': null,
        'client_id': null,
        'client_secret': null,
        'uri': 'http://localhost:8888'
    },
    'label': 'p-config-server',
    'name': 'My Config Server',
    'plan': 'standard',
    'tags': [
        'configuration',
        'spring-cloud'
        ]
    }
    ]
}";

            System.Environment.SetEnvironmentVariable("VCAP_APPLICATION", VCAP_APPLICATION);
            System.Environment.SetEnvironmentVariable("VCAP_SERVICES", VCAP_SERVICES);

            var builder = new WebHostBuilder().UseStartup<TestServerCloudfoundryStartup>()
                                                .UseEnvironment("development");

            // Act and Assert (TestServer expects Spring Cloud Config server to be running @ localhost:8888)
            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();
                string result = await client.GetStringAsync("http://localhost/Home/VerifyAsInjectedOptions");

                Assert.Equal("spam" +
                    "from foo development" +
                    "Spring Cloud Samples" +
                    "https://github.com/spring-cloud-samples", result);
            }
        }

       
        [Fact(Skip = "Requires matching PCF environment with SCCS provisioned")]
        public async void SpringCloudConfigServer_ConfiguredViaCloudfoundryEnv()
        {
            // Arrange
            var VCAP_APPLICATION = @" 
{
    'limits': {
    'mem': 1024,
    'disk': 1024,
    'fds': 16384
    },
    'application_id': 'c2e03250-62e3-4494-82fb-1bc6e2e25ad0',
    'application_version': 'ef087dfd-2955-4854-86c1-4a2cf30e05b3',
    'application_name': 'test',
    'application_uris': [
    'test.apps.testcloud.com'
    ],
    'version': 'ef087dfd-2955-4854-86c1-4a2cf30e05b3',
    'name': 'test',
    'space_name': 'development',
    'space_id': 'ff257d70-eeed-4487-9d6c-4ac709f76aea',
    'uris': [
    'test.apps.testcloud.com'
    ],
    'users': null
}";


            var VCAP_SERVICES = @"
{
    'p-config-server': [
    {
        'name': 'myConfigServer',
        'label': 'p-config-server',
        'tags': [
        'configuration',
        'spring-cloud'
        ],
        'plan': 'standard',
        'credentials': {
        'uri': 'https://config-5b3af2c9-754f-4eb6-9d4b-da50d33d5a5f.apps.testcloud.com',
        'client_id': 'p-config-server-690772bc-2820-4a2c-9c76-6d8ccf8e8de5',
        'client_secret': 'Ib9RFhVPuLub',
        'access_token_uri': 'https://p-spring-cloud-services.uaa.system.testcloud.com/oauth/token'
        }
    }
    ]
}";

            System.Environment.SetEnvironmentVariable("VCAP_APPLICATION", VCAP_APPLICATION);
            System.Environment.SetEnvironmentVariable("VCAP_SERVICES", VCAP_SERVICES);

            var builder = new WebHostBuilder().UseStartup<TestServerCloudfoundryStartup>()
                                                .UseEnvironment("development");

            // Act and Assert (TestServer expects Spring Cloud Config server to be running)
            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();
                string result = await client.GetStringAsync("http://localhost/Home/VerifyAsInjectedOptions");

                Assert.Equal("spam" +
                    "barcelona" +
                    "Spring Cloud Samples" +
                    "https://github.com/spring-cloud-samples", result);
            }
        }
    }
}

