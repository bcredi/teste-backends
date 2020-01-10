using System;
using System.Linq;

namespace Solution.Domain.Events.Warranty
{
   public class WarrantyUpdatedEvent : EventBase
   {
      public Guid WarrantyId { get; set; }
      public decimal Value { get; set; }
      public string Province { get; set; }
      
      public WarrantyUpdatedEvent(IProposalRepository repo, string[] messageData): base (repo, messageData)
      {
         WarrantyId = Guid.Parse(messageData[5]);
         Value = decimal.Parse(messageData[6]);
         Province = messageData[7].Trim();
      }

      public override void Run()
      {
         var currentProposal = _repo.GetById(this.ProposalId);
         var warranty = currentProposal.Warranties.Single(w => w.Id == this.WarrantyId);
         _repo.Update(currentProposal);
      }
   }
}