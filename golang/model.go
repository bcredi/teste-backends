package solution

import (
	"fmt"
	"strconv"
	"time"
)

//Proposal entity that represents entire proposal
type Proposal struct {
	ID           string
	LoanValue    float64
	Installments int
	Warranty     []Warranty
	Proponent    []Proponent
}

type Event struct {
	ID         string
	ProposalID string
	Schema     string
	Action     string
	Timestamp  time.Time
}

type Warranty struct {
	ID       string
	Value    float64
	Province string
}

type Proponent struct {
	ID            string
	Name          string
	Age           int
	MonthlyIncome float64
	IsMain        bool
}

func parseEvent(message []string) Event {
	if len(message) < 5 {
		return Event{}
	}
	timestamp, _ := time.Parse(time.RFC3339, message[3])
	return Event{
		ID:         message[0],
		Schema:     message[1],
		Action:     message[2],
		Timestamp:  timestamp,
		ProposalID: message[4],
	}
}

func newestEvent(newEvent Event, events map[string]Event) map[string]Event {
	for index, event := range events {
		eventSchema := fmt.Sprintf("%s.%s", event.Schema, event.Action)
		messageSchema := fmt.Sprintf("%s.%s", newEvent.Schema, newEvent.Action)
		if eventSchema == messageSchema && event.Timestamp.Before(newEvent.Timestamp) {
			delete(events, index)
			events[newEvent.ID] = newEvent
			return events
		}
	}
	return events
}

func createProposal(event []string, proposals map[string]Proposal) map[string]Proposal {
	installments, _ := strconv.Atoi(event[6])
	loanValue, _ := strconv.ParseFloat(event[5], 64)
	if proposal, ok := proposals[event[4]]; ok {
		proposal.Installments = installments
		proposal.LoanValue = loanValue
		proposals[event[4]] = proposal
		return proposals
	}
	proposals[event[4]] = Proposal{
		ID:           event[4],
		Installments: installments,
		LoanValue:    loanValue,
	}
	return proposals
}

func createWarranty(event []string, proposals map[string]Proposal) map[string]Proposal {
	value, _ := strconv.ParseFloat(event[6], 64)
	warranty := Warranty{
		ID:       event[5],
		Value:    value,
		Province: event[7],
	}
	if proposal, ok := proposals[event[4]]; ok {
		proposal.Warranty = append(proposal.Warranty, warranty)
		proposals[event[4]] = proposal
		return proposals
	}
	proposals[event[4]] = Proposal{
		Warranty: append([]Warranty{}, warranty),
	}
	return proposals
}

func removeWarranty(event []string, proposals map[string]Proposal) map[string]Proposal {
	if proposal, ok := proposals[event[4]]; ok {
		warrantyList := proposals[event[4]].Warranty
		for index, warranty := range warrantyList {
			if event[5] == warranty.ID {
				proposal.Warranty = append(warrantyList[:index], warrantyList[index+1:]...)
			}
		}
		proposals[event[4]] = proposal
		return proposals
	}
	return proposals

}

func createProponent(event []string, proposals map[string]Proposal) map[string]Proposal {
	age, _ := strconv.Atoi(event[7])
	monthlyIncome, _ := strconv.ParseFloat(event[8], 64)
	isMain, _ := strconv.ParseBool(event[9])
	proponent := Proponent{
		ID:            event[5],
		Name:          event[6],
		Age:           age,
		MonthlyIncome: monthlyIncome,
		IsMain:        isMain,
	}
	if proposal, ok := proposals[event[4]]; ok {
		proposal.Proponent = append(proposal.Proponent, proponent)
		proposals[event[4]] = proposal
		return proposals
	}
	proposals[event[4]] = Proposal{
		Proponent: append([]Proponent{}, proponent),
	}
	return proposals
}

func isValidLoanValue(value float64) bool {
	if value >= float64(30000) && value <= float64(3000000) {
		return true
	}
	return false
}

func isValidInstallments(installment int) bool {
	if installment >= 24 && installment <= 180 {
		return true
	}
	return false
}

func isValidProponent(proposal Proposal) bool {
	list := proposal.Proponent
	if len(list) < 2 {
		return false
	}
	mainProponentQty := 0
	mainMonthlyIncome := float64(0)
	mainAge := 0
	for _, proponent := range list {
		if proponent.Age < 18 {
			return false
		}
		if proponent.IsMain {
			mainProponentQty++
			mainAge = proponent.Age
			mainMonthlyIncome = proponent.MonthlyIncome
		}
	}
	if mainProponentQty != 1 {
		return false
	}
	if !isValidMonthlyIncome((proposal.LoanValue / float64(proposal.Installments)), mainAge, mainMonthlyIncome) {
		return false
	}
	return true
}

func isValidWarranty(proposal Proposal) bool {
	list := proposal.Warranty
	if len(list) < 1 {
		return false
	}
	totalWarrantyAmount := float64(0)
	for _, warranty := range list {
		if isDeniedProvince(warranty.Province) {
			continue
		}
		totalWarrantyAmount += warranty.Value
	}
	return totalWarrantyAmount >= (2 * proposal.LoanValue)
}

func isValidMonthlyIncome(installmentValue float64, age int, monthlyIncome float64) bool {
	if age > 18 && age <= 24 {
		return monthlyIncome >= (4 * installmentValue)
	}
	if age > 24 && age <= 50 {
		return monthlyIncome >= (3 * installmentValue)
	}
	return monthlyIncome >= (2 * installmentValue)
}

func isDeniedProvince(province string) bool {
	switch province {
	case "PR", "SC", "RS":
		return true
	}
	return false
}
