﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace Hutech.Infrastructure.CompiledModels
{
    public partial class ApplicationDbContextModel
    {
        partial void Initialize()
        {
            var category = CategoryEntityType.Create(this);
            var product = ProductEntityType.Create(this);
            var applicationUser = ApplicationUserEntityType.Create(this);
            var identityRole = IdentityRoleEntityType.Create(this);
            var identityRoleClaim = IdentityRoleClaimEntityType.Create(this);
            var identityUserClaim = IdentityUserClaimEntityType.Create(this);
            var identityUserLogin = IdentityUserLoginEntityType.Create(this);
            var identityUserRole = IdentityUserRoleEntityType.Create(this);
            var identityUserToken = IdentityUserTokenEntityType.Create(this);

            ProductEntityType.CreateForeignKey1(product, category);
            IdentityRoleClaimEntityType.CreateForeignKey1(identityRoleClaim, identityRole);
            IdentityUserClaimEntityType.CreateForeignKey1(identityUserClaim, applicationUser);
            IdentityUserLoginEntityType.CreateForeignKey1(identityUserLogin, applicationUser);
            IdentityUserRoleEntityType.CreateForeignKey1(identityUserRole, identityRole);
            IdentityUserRoleEntityType.CreateForeignKey2(identityUserRole, applicationUser);
            IdentityUserTokenEntityType.CreateForeignKey1(identityUserToken, applicationUser);

            CategoryEntityType.CreateAnnotations(category);
            ProductEntityType.CreateAnnotations(product);
            ApplicationUserEntityType.CreateAnnotations(applicationUser);
            IdentityRoleEntityType.CreateAnnotations(identityRole);
            IdentityRoleClaimEntityType.CreateAnnotations(identityRoleClaim);
            IdentityUserClaimEntityType.CreateAnnotations(identityUserClaim);
            IdentityUserLoginEntityType.CreateAnnotations(identityUserLogin);
            IdentityUserRoleEntityType.CreateAnnotations(identityUserRole);
            IdentityUserTokenEntityType.CreateAnnotations(identityUserToken);

            AddAnnotation("ProductVersion", "7.0.9");
            AddAnnotation("Relational:MaxIdentifierLength", 128);
            AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}