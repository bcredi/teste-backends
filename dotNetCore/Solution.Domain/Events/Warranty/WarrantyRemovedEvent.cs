using System;

namespace Solution.Domain.Events.Warranty
{
   public class WarrantyRemovedEvent : EventBase
   {
      public Guid WarrantyId { get; set; }
   }
}