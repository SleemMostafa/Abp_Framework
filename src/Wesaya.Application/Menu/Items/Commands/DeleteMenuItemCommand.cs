using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Items.Commands;

public record DeleteMenuItemCommand(Guid Id) : IRequest;

public class DeleteMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<DeleteMenuItemCommand>
{
    public async Task Handle(
        DeleteMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));

        await menuItemRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
    }
}
