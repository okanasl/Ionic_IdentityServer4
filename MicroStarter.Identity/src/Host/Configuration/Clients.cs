// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Host.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "IoClient_Id",
                    ClientName = "IoClient",
                    ClientUri = "http://localhost:8000",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RequireConsent = false,                    
                    AccessTokenType = AccessTokenType.Reference,            
                    RedirectUris = 
                    {
                       "http://localhost:8000",
						"http://localhost:8000/silent-renew.html",
                    },
                    PostLogoutRedirectUris =
                    { 
                        "http://localhost:8000",
						"http://localhost:8000/loggedout"
                    },
                    AllowedCorsOrigins =
                    { 
                        "http://localhost:8000",

                    },
                    AllowedScopes =
                    {
                        "openid",
						"email",
						"profile"

                    }
                }
            };
        }
    }
}