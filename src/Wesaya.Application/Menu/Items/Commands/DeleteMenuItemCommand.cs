using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Items.Commands;

public record DeleteMenuItemCommand(Guid Id) : IRequest;

public class DeleteMenuItemCommandValidator : AbstractValidator<DeleteMenuItemCommand>
{
    public DeleteMenuItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<DeleteMenuItemCommand>
{
    public async Task Handle(
        DeleteMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        await menuItemRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
    }
}
