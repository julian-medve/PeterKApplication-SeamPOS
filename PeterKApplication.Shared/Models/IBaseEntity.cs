using System;

namespace PeterKApplication.Shared.Models
{
    public interface IBaseEntity
    {
        DateTime? CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
        bool IsSynced { get; set; }
    }
}