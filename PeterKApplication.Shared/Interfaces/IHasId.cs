using System;

namespace PeterKApplication.Shared.Interfaces
{
    public interface IHasId
    {
        Guid Id { get; set; }
    }
}