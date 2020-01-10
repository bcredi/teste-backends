using System;

namespace Solution.Domain.Events.Proposal
{
   public class ProposalUpdatedEvent : EventBase
   {
      public decimal LoanValue { get; set; }
      public int NumberOfInstallments { get; set; }
      public ProposalUpdatedEvent(IProposalRepository repo,string[] messageData): base(repo,messageData)
      {
         LoanValue = decimal.Parse(messageData[5]);
         NumberOfInstallments = Int32.Parse(messageData[6]);
      }

      public override void Run()
      {
         if (!IsValid())
            return;

         var currentProposal = _repo.GetById(Id);
         currentProposal.LoanValue = LoanValue;
         currentProposal.NumberOfInstallments = NumberOfInstallments;

         _repo.Update(currentProposal);
      }
   }
}