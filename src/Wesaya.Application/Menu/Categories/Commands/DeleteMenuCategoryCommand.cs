using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Categories.Commands;

public record DeleteMenuCategoryCommand(Guid Id) : IRequest;

public class DeleteMenuCategoryCommandHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<DeleteMenuCategoryCommand>
{
    public async Task Handle(
        DeleteMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));

        await categoryRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
    }
}
