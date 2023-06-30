using System.Xml.Linq;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Shared.BaseClasses;

public abstract class BaseEntity
{
    private Guid _id = Guid.Empty;
    public virtual Guid Id
    {
        get => _id;
        protected set
        {
            if (value == Guid.Empty)
            {
                throw new DomainValidationException("The id of an entity must be defined for it to be instanciated");
            }
            _id = value;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is not BaseEntity))
            return false;
        if (GetType() != obj.GetType())
            return false;

        BaseEntity item = (BaseEntity)obj;
        return item.Id == Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public static bool operator ==(BaseEntity left, BaseEntity right)
    {
        if (Equals(left, null))
            return Equals(right, null);
        else
            return left.Equals(right);
    }
    public static bool operator !=(BaseEntity left, BaseEntity right)
    {
        return !(left == right);
    }
}