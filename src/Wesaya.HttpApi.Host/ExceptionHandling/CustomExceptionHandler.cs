using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;
using Wesaya.Localization;
using Wesaya.Validation;
using FluentValidationException = FluentValidation.ValidationException;

namespace Wesaya.ExceptionHandling;

public sealed class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger,
    IStringLocalizer<WesayaResource> localizer)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Error Message: {ExceptionMessage}, Time of occurrence {Time}",
            exception.Message,
            DateTime.UtcNow);

        var details = GetErrorDetails(exception);

        context.Response.StatusCode = details.StatusCode;

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        if (!string.IsNullOrWhiteSpace(details.Code))
        {
            problemDetails.Extensions.Add("code", details.Code);
        }

        var validationErrors = GetValidationErrors(exception);
        if (validationErrors.Count > 0)
        {
            problemDetails.Extensions.Add("validationErrors", validationErrors);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private (string Detail, string Title, int StatusCode, string? Code) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            WesayaValidationException validationException => (
                Localize(validationException.Code, "Your request is not valid."),
                "ValidationError",
                StatusCodes.Status400BadRequest,
                validationException.Code),

            AbpValidationException => (
                localizer["Your request is not valid."],
                "ValidationError",
                StatusCodes.Status400BadRequest,
                WesayaErrorCodes.ValidationError),

            FluentValidationException => (
                localizer["Your request is not valid."],
                "ValidationError",
                StatusCodes.Status400BadRequest,
                WesayaErrorCodes.ValidationError),

            EntityNotFoundException => (
                localizer["EntityNotFound"],
                "NotFound",
                StatusCodes.Status404NotFound,
                null),

            BusinessException businessException => (
                Localize(businessException.Code, businessException.Message),
                businessException.GetType().Name,
                GetBusinessExceptionStatusCode(businessException),
                businessException.Code),

            AbpAuthorizationException => (
                localizer["AuthorizationFailed"],
                "AuthorizationError",
                StatusCodes.Status403Forbidden,
                null),

            _ => (
                localizer["InternalServerError"],
                "InternalServerError",
                StatusCodes.Status500InternalServerError,
                null)
        };
    }

    private int GetBusinessExceptionStatusCode(BusinessException exception)
    {
        return exception.Code switch
        {
            WesayaErrorCodes.MenuCategoryNotFound => StatusCodes.Status404NotFound,
            WesayaErrorCodes.MenuItemNotFound => StatusCodes.Status404NotFound,
            WesayaErrorCodes.ExtraItemNotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };
    }

    private string Localize(string? code, string fallbackMessage)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return fallbackMessage;
        }

        var localizedMessage = localizer[code];

        return localizedMessage.ResourceNotFound
            ? fallbackMessage
            : localizedMessage.Value;
    }

    private static List<object> GetValidationErrors(Exception exception)
    {
        return exception switch
        {
            IHasValidationErrors validationException => validationException.ValidationErrors
                .Select(ToValidationError)
                .ToList(),

            FluentValidationException fluentValidationException => fluentValidationException.Errors
                .Select(error => new
                {
                    message = error.ErrorMessage,
                    members = string.IsNullOrWhiteSpace(error.PropertyName)
                        ? []
                        : new[] { error.PropertyName }
                })
                .Cast<object>()
                .ToList(),

            _ => []
        };
    }

    private static object ToValidationError(ValidationResult validationResult)
    {
        return new
        {
            message = validationResult.ErrorMessage,
            members = validationResult.MemberNames.ToArray()
        };
    }
}
