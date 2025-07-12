using Contract.Common.Interfaces;
using Contract.Domain.Interfaces;

namespace Contract.Common.Events
{
    public abstract class AuditableEventEntity<T> : EventEntity<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
