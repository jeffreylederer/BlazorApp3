﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp3.Models.DTOs
{
    public class UserDTOEdit
    {
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Role")]
        public int Role { get; set; }

        public string? RoleName { get; set; }
    }
}
