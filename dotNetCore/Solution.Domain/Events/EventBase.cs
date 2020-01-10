using System;
using System.Linq;

namespace Solution.Domain.Events
{
   public abstract class EventBase
   {
      protected readonly IProposalRepository _repo;

      public string Schema { get; }
      public string Action { get; }
      public Guid Id { get; set; }
      public DateTimeOffset Timestamp { get; set; }
      public Guid ProposalId { get; set; }
      public EventBase(IProposalRepository repo, string[] messageData){
         _repo = repo;
         Schema = messageData[1];
         Action = messageData[2];
         Id = Guid.Parse(messageData[0]);
         Timestamp = DateTime.Parse(messageData[3]);
         ProposalId = Guid.Parse(messageData[4]);

      }

      public abstract void Run();

      public virtual bool IsValid()
      {
         var currentProposal = _repo.GetById(Id);

         if (currentProposal.Events.Any(ev => ev.Timestamp > Timestamp))
            return false;
         
         if (currentProposal.Events.Any(ev => ev.Id == Id))
            return false;

         return true;
      }

   }
}