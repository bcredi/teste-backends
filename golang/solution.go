package solution

import (
	"fmt"
	"log"
	"sort"
	"strings"
)

func ProcessMessages(messages []string) string {
	proposals := mountProposals(messages)
	return validateProposals(proposals)
}

func mountProposals(messages []string) map[string]Proposal {
	proposalsList := map[string]Proposal{}
	processedEvents := map[string]Event{}
	for _, message := range messages {
		messageItens := strings.Split(message, ",")

		if _, exist := processedEvents[messageItens[0]]; exist {
			log.Println("Repeated event. Do nothing")
			continue
		}
		processedEvents[messageItens[0]] = parseEvent(messageItens)
		proposalsList = applyEvent(messageItens, proposalsList)
	}
	return proposalsList
}

func validateProposals(proposals map[string]Proposal) (validProposals string) {
	validatedList := []string{}
	for id, proposal := range proposals {
		if !isValidLoanValue(proposal.LoanValue) {
			continue
		}
		if !isValidInstallments(proposal.Installments) {
			continue
		}
		if !isValidProponent(proposal) {
			continue
		}
		if !isValidWarranty(proposal) {
			continue
		}
		validatedList = append(validatedList, id)
	}
	sort.Strings(validatedList)
	return strings.Join(validatedList, ",")
}

func applyEvent(event []string, proposals map[string]Proposal) map[string]Proposal {
	eventType := fmt.Sprintf("%s.%s", event[1], event[2])
	switch eventType {
	case "proposal.created":
		proposals = createProposal(event, proposals)
	case "warranty.added":
		proposals = createWarranty(event, proposals)
	case "warranty.removed":
		proposals = removeWarranty(event, proposals)
	case "proponent.added":
		proposals = createProponent(event, proposals)
	}
	return proposals
}
