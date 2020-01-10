using System;

namespace Solution.Domain.Events.Proponent
{
   public class ProponentRemovedEvent : EventBase
   {
      public Guid ProponentId { get; set; }
   }
}