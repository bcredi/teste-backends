using System;

namespace Solution.Domain.Events
{
   public abstract class EventBase
   {
      public string Schema { get; }
      public string Action { get; }
      public Guid Id { get; set; }
      public DateTimeOffset Timestamp { get; set; }
      public Guid ProposalId { get; set; }
   }
}