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
      public ProponentUpdatedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData)
      {
         ProponentId = Guid.Parse(messageData[5]);
         Name = messageData[6].Trim();
         Age = Int32.Parse(messageData[7]);
         MonthlyIncome = Decimal.Parse(messageData[8]);
         IsMain = bool.Parse(messageData[9]);
      }

      public override void Run()
      {
      }
   }
}