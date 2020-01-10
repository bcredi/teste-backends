using System;

namespace Solution.Domain.Events.Warranty
{
   public class WarrantyUpdatedEvent : EventBase
   {
      public Guid WarrantyId { get; set; }
      public decimal Value { get; set; }
      public string Province { get; set; }
   }
}