using System;
using System.Linq;

namespace Solution.Domain.Events.Warranty
{
   public class WarrantyRemovedEvent : EventBase
   {
      public Guid WarrantyId { get; set; }
      public WarrantyRemovedEvent(IProposalRepository repo, string[] messageData) : base (repo, messageData)
      {
         WarrantyId = Guid.Parse(messageData[5]);
      }

      public override void Run()
      {
         var currentProposal = _repo.GetById(this.ProposalId);
         var warranty = currentProposal.Warranties.Single(w => w.Id == this.WarrantyId);
         currentProposal.Warranties.Remove(warranty);
         _repo.Update(currentProposal);
      }
   }
}