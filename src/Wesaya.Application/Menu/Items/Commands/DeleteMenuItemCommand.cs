using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Items.Commands;

public record DeleteMenuItemCommand(Guid Id) : IRequest;

public class DeleteMenuItemCommandValidator : AbstractValidator<DeleteMenuItemCommand>
{
    public DeleteMenuItemCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["MenuItem:IdRequired"]);
    }
}

public class DeleteMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<DeleteMenuItemCommand>
{
    public async Task Handle(
        DeleteMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuItemNotFoundException();

        await menuItemRepository.DeleteAsync(item, cancellationToken: cancellationToken);
    }
}
