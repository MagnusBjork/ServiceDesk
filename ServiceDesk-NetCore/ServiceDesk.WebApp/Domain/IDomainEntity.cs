using System;

namespace ServiceDesk.WebApp.Domain
{
    public interface IDomainEntity
    {
        Guid Id { get; set; }
    }
}