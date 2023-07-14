namespace YourBuddyPull.Application.Contracts.Data;

public interface IUnitOfWork
{
    void OpenTransaction();
    void CommitTransaction();
    void AbortTransaction();
}
