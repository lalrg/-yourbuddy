using MediatR;
using MediatR.Pipeline;
using System;
using YourBuddyPull.Application.Contracts.Configuration;
using YourBuddyPull.Application.Contracts.EmailSender;

namespace YourBuddyPull.Application.ExceptionHandling;

public class ExceptionLoggingHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
         where TRequest : IRequest<TResponse>
         where TException : Exception
{
    private readonly IEmailSender _emailSender;
    private readonly IConfigurationProvider _configurationProvider;
    public ExceptionLoggingHandler(IEmailSender emailSender, IConfigurationProvider configurationProvider)
    {
        _emailSender = emailSender;
        _configurationProvider = configurationProvider;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        //if(exception.GetType() == typeof(DomainValidationException))
        //{
        //    throw exception;
        //}

        try
        {
            // _emailSender.SendMail(_configurationProvider.MailSenderUsername(), "Ha ocurrido un error inesperado en la aplicacion");
        }
        catch {}

        throw new ApplicationException(exception.Message);
    }
}