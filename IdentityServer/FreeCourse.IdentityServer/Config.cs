// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("photo_stock_catalog"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };


        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),//email claim
                       new IdentityResources.OpenId(),//mutlaka olmak zorunda!JWT pair olduğunda subclaim'e karşılık geliyor.
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı rolleri",UserClaims=new[]{"role"}}
               
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName}
                },
                  new Client
                {
                    ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//GrantTypes=Akış Tipi,ResourceOwnerPasswordAndClientCredentials'da Refresh token yoktur.
                    AllowedScopes={ IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,"roles"},
                    //Refresh token elimizde varsa kullanıcı login olmasa bile kullanıcı adına refresh token alabiliriz.O yüzden ismi offlineAccess'dir.
                    AccessTokenLifetime=1*60*60,//1 saat
                    RefreshTokenExpiration=TokenExpiration.Absolute,//61.gün ömrü dolmuş olacak.
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,//60 gün
                    RefreshTokenUsage=TokenUsage.ReUse //RefreshToken tekrar kullanılabilir.
                  }

            };
    }
}