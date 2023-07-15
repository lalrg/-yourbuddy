namespace YourBuddyPull.Application.Contracts.Data;

public interface IUnitOfWork
{
    void OpenTransaction();
    Task CommitTransaction();
    void AbortTransaction();
}
