using YourBuddyPull.Application.Contracts.Data;

namespace YourBuddyPull.Repository.SQLServer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProyectoLuisRContext _context;
    public UnitOfWork(ProyectoLuisRContext context)
    {
        _context = context;
    }
    public void AbortTransaction()
    {
    }

    public async Task CommitTransaction()
    {
        await _context.SaveChangesAsync();
    }

    public void OpenTransaction()
    {
    }
}
