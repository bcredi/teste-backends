using System;

namespace Solution.Domain.Events.Proposal
{
   public class ProposalCreatedEvent : EventBase
   {
      public decimal LoanValue { get; set; }
      public int NumberOfInstallments { get; set; }
   }
}