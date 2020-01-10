namespace Solution.Domain.Events.Proposal
{
   public class ProposalDeletedEvent : EventBase
   {
      public ProposalDeletedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData){  }

      public override void Run()
      {
         if (!IsValid())
            return;

         _repo.Delete(this.ProposalId);
      }
      public override bool IsValid()
      {
         var currentProposal = _repo.GetById(Id);

         if (currentProposal != null) 
            return base.IsValid();

         return false;
      }
   }
}