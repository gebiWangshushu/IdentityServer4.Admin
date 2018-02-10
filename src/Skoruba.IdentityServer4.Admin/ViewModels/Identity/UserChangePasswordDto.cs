﻿using System.ComponentModel.DataAnnotations;
using Skoruba.IdentityServer4.Admin.ViewModels.Identity.Base;

namespace Skoruba.IdentityServer4.Admin.ViewModels.Identity
{
    public class UserChangePasswordDto : BaseUserChangePasswordDto<int>
    {        
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}