using System;

namespace Solution.Domain
{
   public interface IProposalRepository
   {
      Proposal GetById(Guid id);
      void Save(Proposal proposal);
      void Delete(Guid proposalId);
      void Update(Proposal proposalId);
   }
}