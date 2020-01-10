using System;

namespace Solution.Domain.Events.Proponent
{
   public class ProponentUpdatedEvent : EventBase
   {
      public Guid ProponentId { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
      public decimal MonthlyIncome { get; set; }
      public bool IsMain { get; set; }
      
   }
}