using System;

namespace Solution.Domain.Events.Warranty
{
   public class WarrantyAddedEvent : EventBase
   {
      public Guid WarrantyId { get; set; }
      public decimal Value { get; set; }
      public string Province { get; set; }
      public WarrantyAddedEvent(IProposalRepository repo, string[] messageData): base (repo, messageData)
      {
         WarrantyId = Guid.Parse(messageData[5]);
         Value = decimal.Parse(messageData[6]);
         Province = messageData[7].Trim();
      }

      public override void Run()
      {
         var currentProposal = _repo.GetById(this.ProposalId);
         var warranty = new Solution.Domain.Warranty(this);
         currentProposal.Warranties.Add(warranty);
         _repo.Update(currentProposal);
      }
   }
}