using System;

namespace Solution.Domain.Events.Proponent
{
   public class ProponentRemovedEvent : EventBase
   {
      public Guid ProponentId { get; set; }
      public ProponentRemovedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData)
      {
         ProponentId = Guid.Parse(messageData[5]);
      }

      public override void Run()
      {
      }
   }
}