using Ardalis.Result;

namespace Aine.Inventory.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}

