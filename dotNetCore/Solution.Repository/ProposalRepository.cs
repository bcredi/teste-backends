using System;
using System.Collections.Generic;
using System.Linq;
using Solution.Domain;

namespace Solution.Repository
{
   public class ProposalRepository : IProposalRepository
   {
      private readonly List<Proposal> proposals;

      public ProposalRepository()
      {
         proposals = new List<Proposal>();
      }

      public void Delete(Guid proposalId)
      {
         var proposal = proposals.Single(p => p.Id == proposalId);
         proposals.Remove(proposal);
      }

      public Proposal GetById(Guid id)
      {
         return proposals.SingleOrDefault(p => p.Id == id);
      }

      public void Save(Proposal proposal)
      {
         proposals.Add(proposal);
      }

      public void Update(Proposal proposal)
      {
         Delete(proposal.Id);
         Save(proposal);
      }
   }
}
