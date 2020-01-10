using System;
using System.Collections.Generic;
using System.Linq;
using Solution.Domain;
using Solution.Domain.Events;
using Solution.Domain.Events.Proponent;
using Solution.Domain.Events.Proposal;
using Solution.Domain.Events.Warranty;

namespace SolutionApp
{
   public class Solution
   {

      private static Solution instance;
      private readonly IProposalRepository _repo;

      private Solution(IProposalRepository repository)
      {
         _repo = repository;
      }

      public static Solution Instance(IProposalRepository repository)
      {
         if (instance == null)
         {
            instance = new Solution(repository);
         }
         return instance;
      }
      //   # Essa função recebe uma lista de mensagens, por exemplo:
      //   #
      //   # [
      //   #   "72ff1d14-756a-4549-9185-e60e326baf1b,proposal,created,2019-11-11T14:28:01Z,80921e5f-4307-4623-9ddb-5bf826a31dd7,1141424.0,240",
      //   #   "af745f6d-d5c0-41e9-b04f-ee524befa425,warranty,added,2019-11-11T14:28:01Z,80921e5f-4307-4623-9ddb-5bf826a31dd7,31c1dd83-8fb7-44ff-8cb7-947e604f6293,3245356.0,DF",
      //   #   "450951ee-a38d-475c-ac21-f22b4566fb29,warranty,added,2019-11-11T14:28:01Z,80921e5f-4307-4623-9ddb-5bf826a31dd7,c8753500-1982-4003-8287-3b46c75d4803,3413113.45,DF",
      //   #   "66882b68-baa4-47b1-9cc7-7db9c2d8f823,proponent,added,2019-11-11T14:28:01Z,80921e5f-4307-4623-9ddb-5bf826a31dd7,3f52890a-7e9a-4447-a19b-bb5008a09672,Ismael Streich Jr.,42,62615.64,true"
      //   # ]
      //   #
      //   # Complete a função para retornar uma string com os IDs das propostas válidas no seguinte formato (separado por vírgula):
      //   # "52f0b3f2-f838-4ce2-96ee-9876dd2c0cf6,51a41350-d105-4423-a9cf-5a24ac46ae84,50cedd7f-44fd-4651-a4ec-f55c742e3477"

      public string ProcessMessages(IEnumerable<string> messages)
      {
         IDictionary<Guid, Proposal> proposals = new Dictionary<Guid, Proposal>();

         foreach (var message in messages)
         {
            EventBase ev = Parse(message);
            ev.Run();
            var proposal = _repo.GetById(ev.ProposalId);
            proposals[proposal.Id] = proposal;
         }

         var validProposals = proposals
            .Where(p => p.Value.IsValid())
            .Select(p => p.Key.ToString());

         return string.Join(',', validProposals);
      }

      public EventBase Parse(string message)
      {
         var messageData = message.Split(',');
         switch (messageData[1])
         {
            case "proposal":
               return ProposalEventBuild(messageData);
            case "proponent":
               return ProponentEventBuild(messageData);
            case "warranty":
               return WarrantyEventBuild(messageData);
            default:
               throw new ArgumentException($"Message schema {messageData[1]} not found");
         }
      }

      private EventBase WarrantyEventBuild(string[] messageData)
      {
         switch (messageData[2])
         {
            case "added":
               return new WarrantyAddedEvent(_repo,messageData);
            case "updated":
               return new WarrantyUpdatedEvent(_repo,messageData);
            case "removed":
               return new WarrantyRemovedEvent(_repo,messageData);
            default:
               throw new ArgumentException($"Message action {messageData[2]} not found for schema {messageData[1]}");
         }
      }

      private EventBase ProponentEventBuild(string[] messageData)
      {
         switch (messageData[2])
         {
            case "added":
               return new ProponentAddedEvent(_repo,messageData);
            case "updated":
               return new ProponentUpdatedEvent(_repo,messageData);
            case "removed":
               return new ProponentRemovedEvent(_repo,messageData);
            default:
               throw new ArgumentException($"Message action {messageData[2]} not found for schema {messageData[1]}");
         }
      }

      private EventBase ProposalEventBuild(string[] messageData)
      {
         switch (messageData[2])
         {
            case "created":
               return new ProposalCreatedEvent(_repo,messageData);
            case "updated":
               return new ProposalUpdatedEvent(_repo,messageData);
            case "deleted":
               return new ProposalDeletedEvent(_repo,messageData);
            default:
               throw new ArgumentException($"Message action {messageData[2]} not found for schema {messageData[1]}");
         }
      }
   }

}