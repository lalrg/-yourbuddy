namespace YourBuddyPull.Application.DTOs.Shared;

public struct PaginationResultDTO<T> where T : struct
{
    public int CurrentPage;
    public int PageSize;
    public int TotalCount;
    public List<T> Items;
}
