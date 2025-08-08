using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for persisting and retrieving <see cref="Sale"/> entities.
    /// Provides methods to add, update, retrieve, and delete sales in the data store.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale to the data store.
        /// </summary>
        /// <param name="sale">The <see cref="Sale"/> entity to add.</param>
        Task AddAsync(Sale sale, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <returns>The <see cref="Sale"/> entity if found; otherwise, null.</returns>
        Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing sale in the data store.
        /// </summary>
        /// <param name="sale">The <see cref="Sale"/> entity with updated information.</param>
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a sale from the data store by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
