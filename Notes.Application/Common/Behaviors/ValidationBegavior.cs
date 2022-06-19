using FluentValidation;
using MediatR;

namespace Notes.Application.Common.Behaviors
{
    public class ValidationBegavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBegavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;


        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(faillure => faillure != null)
                .ToList();
            if (failures.Count != null)
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}
