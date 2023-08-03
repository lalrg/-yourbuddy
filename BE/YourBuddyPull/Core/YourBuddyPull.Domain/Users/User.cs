
using System.Text.RegularExpressions;
using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Users;

public sealed class User : BaseEntity
{
    private User(Guid id, string name, string lastName, string email, List<Role> roles)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        IsDeleted = false;
        _roles = roles;
    }
    private string _name = string.Empty;
    public string Name { get => _name; private set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("Name is required");
            else
                _name = value;
        }  
    }
    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName; private set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("Last Name is required");
            else
                _lastName = value;
        }
    }
    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainValidationException("The Email field cannot be null");

            }
            Regex rx = new Regex(
                @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            if (rx.IsMatch(value))
            {
                _email = value;
            }
            else
            {
                throw new DomainValidationException("The Email field must be a valid email");
            }
        }
    }
    public bool IsDeleted
    {
        get => _isDeleted; private set
        {
            if (_isDeleted)
            {
                throw new DomainValidationException("The user is already deleted");
            }
            else
            {
                _isDeleted = value;
            }
        }
    }
    private bool _isDeleted = false;
    private List<Role> _roles { get; set; } = new();
    public IReadOnlyCollection<Role> Roles
    {
        get => _roles;
    }

    public static User Instanciate(Guid id, string name, string lastName, string email, bool isDeleted, List<Role> roles)
    {
        if (isDeleted)
            throw new DomainValidationException("You cannot instanciate an user with a deleted status");

        if (roles is null)
            throw new DomainValidationException("You cannot instanciate an user without roles");

        User user = new(id, name, lastName, email, roles);

        return user;
    }

    public static User Create(string name, string lastName, string email, List<Role> roles)
    {
        if (roles is null)
            throw new DomainValidationException("You cannot create an user without roles");

        User user = new(Guid.NewGuid(), name, lastName, email, roles);

        return user;
    }

    public void UpdateProperties(string name, string lastname, string email)
    {
        Name = name;
        LastName = lastname;
        Email = email;
    }
    public void SetAsDeleted()
    {
        IsDeleted = true;
    }
    public void AddRole(Role role)
    {
        _roles.Add(role);
    }
    public void RemoveRole(Role role)
    {
        if(_roles.Count() < 2)
        {
            throw new DomainValidationException("The user only has one role, you cannot leave a user without roles");
        }
        var roleToRemove = _roles.Single(x => x == role) ?? 
            throw new DomainValidationException("The role that you are trying to remove is not assigned to this user");

        _roles.Remove(roleToRemove);
    }
}
