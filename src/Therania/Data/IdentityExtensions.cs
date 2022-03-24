using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
namespace Therania.Data;

    public static class IdentityExtensions
    {
        public static IdentityBuilder AddSecondIdentity<TUser>(
            this IServiceCollection services)
            where TUser : class
        {
            // services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            // services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            // services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            // services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            // services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser>>();
            // services.TryAddScoped<UserManager<TUser>, AspNetUserManager<TUser>>();
            // services.TryAddScoped<SignInManager<TUser>, SignInManager<TUser>>();

            return new IdentityBuilder(typeof(TUser), services);
        }
    }
