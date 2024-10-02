using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;
using System.Windows.Input;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.Username).MinimumLength(10).WithMessage("username is required.");
        }
    }
    public class DeleteBasketCommandhandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeletBasket(command.Username, cancellationToken);
            return new DeleteBasketResult(true);
         }
    }
}
