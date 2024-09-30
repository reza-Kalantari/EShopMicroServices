
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            //var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            //if (product is null) {
            //    throw new ProductNotFoundException();
            //}
            //session.Delete(product);
            session.Delete<Product>(command.id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
