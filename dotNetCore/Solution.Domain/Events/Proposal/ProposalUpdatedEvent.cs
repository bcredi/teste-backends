using System;

namespace Solution.Domain.Events.Proposal
{
   public class ProposalUpdatedEvent : EventBase
   { 
      public decimal LoanValue { get; set; }
      public int NumberOfInstallments { get; set; }

   }
}