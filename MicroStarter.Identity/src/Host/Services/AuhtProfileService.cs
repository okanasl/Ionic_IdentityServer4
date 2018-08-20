using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Entities;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Host.Services
{
    public class AuthProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<AuthProfileService> Logger;
        public AuthProfileService(
            UserManager<ApplicationUser> userManager,
         IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }
        public AuthProfileService(
            UserManager<ApplicationUser> userManager,
         IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
         ILogger<AuthProfileService> logger)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            Logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();

            ApplicationUser user = await _userManager.FindByIdAsync(sub);
            
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            foreach (var item in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, item));
            }
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            context.IssuedClaims = claims;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject Id claim present");

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }
    }
}