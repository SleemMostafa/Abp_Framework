using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Categories.Commands;

public record DeleteMenuCategoryCommand(Guid Id) : IRequest;

public class DeleteMenuCategoryCommandValidator : AbstractValidator<DeleteMenuCategoryCommand>
{
    public DeleteMenuCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteMenuCategoryCommandHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<DeleteMenuCategoryCommand>
{
    public async Task Handle(
        DeleteMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        await categoryRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
    }
}
