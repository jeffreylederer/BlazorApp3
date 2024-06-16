using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<UserRole> RolesNavigator { get; set; } = new List<UserRole>();
}
