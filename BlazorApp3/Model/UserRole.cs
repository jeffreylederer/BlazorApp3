using BlazorApp3.Model;
using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }


    public virtual User? User { get; set; } = null;
    public virtual Role? Role { get; set; } = null;


}
