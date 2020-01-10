using System;
using System.Collections.Generic;
using Solution.Domain.Events;
using Solution.Domain.Events.Proposal;

namespace Solution.Domain
{
   public class Proposal
   {
      public Guid Id { get; set; }
      public decimal LoanValue { get; set; }
      public int NumberOfInstallments { get; set; }
      public List<Proponent> Proponents { get; set; }
      public List<Warranty> Warranties { get; set; }
      public List<EventBase> Events { get; set; }

      public Proposal(ProposalCreatedEvent ev)
      {
         Id = ev.ProposalId;
         LoanValue = ev.LoanValue;
         NumberOfInstallments = ev.NumberOfInstallments;
         Events = new List<EventBase>(){ev};
         Proponents = new List<Proponent>();
         Warranties = new List<Warranty>();
      }

      public bool IsValid()
      {
         return true;
      }
   }
}