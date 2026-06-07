using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Exceptions;

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
        var category = await categoryRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuCategoryNotFoundException();

        await categoryRepository.DeleteAsync(category, cancellationToken: cancellationToken);
    }
}
