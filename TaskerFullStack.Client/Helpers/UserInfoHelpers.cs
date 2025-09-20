﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace TaskerFullStack.Client.Helpers
{
    public static class UserInfoHelpers
    {
        public static async Task<UserInfo?> GetUserInfoAsync(Task<AuthenticationState>? authStateTask)
        {
            if (authStateTask == null) return null;

            AuthenticationState authState = await authStateTask;
            ClaimsPrincipal user = authState.User;

            try
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var email = user.FindFirst(ClaimTypes.Email)!.Value;
                var firstName = user.FindFirst("FirstName")!.Value;
                var lastName = user.FindFirst("LastName")!.Value;

                return new UserInfo
                {
                    UserId = userId,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
