using FluentValidation.Results;
using MediatR;
using System;

namespace KlingerStore.Core.Domain.Message
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime TimesTamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimesTamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
