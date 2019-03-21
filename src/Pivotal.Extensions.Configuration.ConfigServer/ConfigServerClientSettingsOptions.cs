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

namespace Pivotal.Extensions.Configuration.ConfigServer
{
    public class ConfigServerClientSettingsOptions
    {
        public bool ValidateCertificates
        {
            get
            {
                return GetBoolean(Spring?.Cloud?.Config?.Validate_Certificates,
                    ConfigServerClientSettings.DEFAULT_CERTIFICATE_VALIDATION);
            }
        }
        public bool Enabled
        {
            get
            {
                return GetBoolean(Spring?.Cloud?.Config?.Enabled,
                    ConfigServerClientSettings.DEFAULT_PROVIDER_ENABLED);
            }
        }
        public bool FailFast
        {
            get
            {
                return GetBoolean(Spring?.Cloud?.Config?.FailFast,
                    ConfigServerClientSettings.DEFAULT_FAILFAST);
            }
        }
        public string Environment
        {
            get
            {
                return Spring?.Cloud?.Config?.Env;
            }
        }
        public string Label
        {
            get
            {
                return Spring?.Cloud?.Config?.Label;
            }

        }
        public string Name
        {
            get
            {
                return Spring?.Cloud?.Config?.Name;
            }

        }
        public string Password
        {
            get
            {
                return Spring?.Cloud?.Config?.Password;
            }

        }
        public string Uri
        {
            get
            {
                return Spring?.Cloud?.Config?.Uri;
            }

        }
        public string Username
        {
            get
            {
                return Spring?.Cloud?.Config?.Username;
            }

        }
        public bool RetryEnabled
        {
            get
            {

                return GetBoolean(Spring?.Cloud?.Config?.Retry?.Enabled,
                    ConfigServerClientSettings.DEFAULT_RETRY_ENABLED);
            }
        }

        public int RetryInitialInterval
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.Retry?.InitialInterval,
               ConfigServerClientSettings.DEFAULT_INITIAL_RETRY_INTERVAL);
            }
        }


        public int RetryMaxInterval
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.Retry?.MaxInterval,
               ConfigServerClientSettings.DEFAULT_MAX_RETRY_INTERVAL);
            }
        }

        public double RetryMultiplier
        {
            get
            {
                return GetDouble(Spring?.Cloud?.Config?.Retry?.Multiplier,
               ConfigServerClientSettings.DEFAULT_RETRY_MULTIPLIER);
            }
        }

        public int RetryAttempts
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.Retry?.MaxAttempts,
               ConfigServerClientSettings.DEFAULT_MAX_RETRY_ATTEMPTS);
            }
        }

        public string Token
        {
            get
            {
                return Spring?.Cloud?.Config?.Token;
            }
        }

        public int TokenTtl
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.TokenTtl,
                ConfigServerClientSettings.DEFAULT_VAULT_TOKEN_TTL);

            }
        }


        public int TokenRenewRate
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.TokenRenewRate,
                        ConfigServerClientSettings.DEFAULT_VAULT_TOKEN_RENEW_RATE);
            }
        }
        public int Timeout
        {
            get
            {
                return GetInt(Spring?.Cloud?.Config?.Timeout,
                    ConfigServerClientSettings.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }

        public string AccessTokenUri
        {
            get
            {
                return Spring?.Cloud?.Config?.Access_Token_Uri;
            }

        }
        public string ClientSecret
        {
            get
            {
                return Spring?.Cloud?.Config?.Client_Secret;
            }

        }
        public string ClientId
        {
            get
            {
                return Spring?.Cloud?.Config?.Client_Id;
            }

        }

        public ConfigServerClientSettings Settings
        {
            get
            {
                ConfigServerClientSettings settings = new ConfigServerClientSettings();

                settings.Enabled = GetBoolean(Spring?.Cloud?.Config?.Enabled,
                    ConfigServerClientSettings.DEFAULT_PROVIDER_ENABLED);
                settings.FailFast = GetBoolean(Spring?.Cloud?.Config?.FailFast,
                    ConfigServerClientSettings.DEFAULT_FAILFAST);
                settings.ValidateCertificates = GetBoolean(Spring?.Cloud?.Config?.Validate_Certificates,
                     ConfigServerClientSettings.DEFAULT_CERTIFICATE_VALIDATION);
                settings.RetryAttempts = GetInt(Spring?.Cloud?.Config?.Retry?.MaxAttempts,
                    ConfigServerClientSettings.DEFAULT_MAX_RETRY_ATTEMPTS);
                settings.RetryEnabled = GetBoolean(Spring?.Cloud?.Config?.Retry?.Enabled,
                     ConfigServerClientSettings.DEFAULT_RETRY_ENABLED);
                settings.RetryInitialInterval = GetInt(Spring?.Cloud?.Config?.Retry?.InitialInterval,
                    ConfigServerClientSettings.DEFAULT_INITIAL_RETRY_INTERVAL);
                settings.RetryMaxInterval = GetInt(Spring?.Cloud?.Config?.Retry?.MaxInterval,
                    ConfigServerClientSettings.DEFAULT_MAX_RETRY_INTERVAL);
                settings.RetryMultiplier = GetDouble(Spring?.Cloud?.Config?.Retry?.Multiplier,
                    ConfigServerClientSettings.DEFAULT_RETRY_MULTIPLIER);
                settings.Timeout = GetInt(Spring?.Cloud?.Config?.Timeout,
                    ConfigServerClientSettings.DEFAULT_TIMEOUT_MILLISECONDS);
                settings.TokenTtl = GetInt(Spring?.Cloud?.Config?.TokenTtl,
                   ConfigServerClientSettings.DEFAULT_VAULT_TOKEN_TTL);
                settings.TokenRenewRate = GetInt(Spring?.Cloud?.Config?.TokenRenewRate,
                    ConfigServerClientSettings.DEFAULT_VAULT_TOKEN_RENEW_RATE);

                settings.Environment = Spring?.Cloud?.Config?.Env;
                settings.Label = Spring?.Cloud?.Config?.Label;
                settings.Name = Spring?.Cloud?.Config?.Name;
                settings.Password = Spring?.Cloud?.Config?.Password;
                settings.Uri = Spring?.Cloud?.Config?.Uri;
                settings.Username = Spring?.Cloud?.Config?.Username;
                settings.Token = Spring?.Cloud?.Config?.Token;
                settings.AccessTokenUri = Spring?.Cloud?.Config?.Access_Token_Uri;
                settings.ClientSecret = Spring?.Cloud?.Config?.Client_Secret;
                settings.ClientId = Spring?.Cloud?.Config?.Client_Id;


                return settings;
            }
        }
        public SpringConfigCloudFoundry Spring { get; set; }

        private bool GetBoolean(string strValue, bool def)
        {

            bool result = def;
            if (!string.IsNullOrEmpty(strValue))
            {
                bool.TryParse(strValue, out result);
            }
            return result;
        }

        private int GetInt(string strValue, int def)
        {
            int result = def;
            if (!string.IsNullOrEmpty(strValue))
            {
                int.TryParse(strValue, out result);
            }
            return result;
        }
        private double GetDouble(string strValue, double def)
        {
            double result = def;
            if (!string.IsNullOrEmpty(strValue))
            {
                double.TryParse(strValue, out result);
            }
            return result;
        }
    }

    public class SpringConfigCloudFoundry
    {
        public CloudConfigCloudFoundry Cloud { get; set; }
    }
    public class CloudConfigCloudFoundry
    {
        public SpringCloudConfigCloudFoundry Config { get; set; }
    }
    public class SpringCloudConfigCloudFoundry
    {
        public SpringCloudConfigCloudFoundryRetry Retry { get; set; }
        public string Enabled { get; set; }
        public string FailFast { get; set; }
        public string Env { get; set; }
        public string Label { get; set; }
        public string Name { get; set;  }
        public string Password { get; set; }
        public string Token { get; set; }
        public string TokenTtl { get; set; }
        public string TokenRenewRate { get; set; }
        public string Timeout { get; set; }
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Access_Token_Uri { get; set; }
        public string Client_Secret { get; set; }
        public string Client_Id { get; set; }
        public string Validate_Certificates { get; set; }

    }
    public class SpringCloudConfigCloudFoundryRetry
    {
        public string Enabled { get; set; }
        public string InitialInterval { get; set; }
        public string MaxInterval { get; set; }
        public string Multiplier { get; set; }
        public string MaxAttempts { get; set; }
    }
}
