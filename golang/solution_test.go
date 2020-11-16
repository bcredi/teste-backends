package solution

import (
	"bufio"
	"log"
	"os"
	"sort"
	"strings"
	"testing"
)

func TestProcessMessages(t *testing.T) {

	input000 := readFile("../test/input/input000.txt")
	input001 := readFile("../test/input/input001.txt")
	input002 := readFile("../test/input/input002.txt")
	input003 := readFile("../test/input/input003.txt")
	input004 := readFile("../test/input/input004.txt")
	input005 := readFile("../test/input/input005.txt")
	input006 := readFile("../test/input/input006.txt")
	input007 := readFile("../test/input/input007.txt")
	input008 := readFile("../test/input/input008.txt")
	//input009 := readFile("../test/input/input009.txt")
	input010 := readFile("../test/input/input010.txt")
	input011 := readFile("../test/input/input011.txt")
	input012 := readFile("../test/input/input012.txt")
	output000 := readFile("../test/output/output000.txt")
	output001 := readFile("../test/output/output001.txt")
	output002 := readFile("../test/output/output002.txt")
	output003 := readFile("../test/output/output003.txt")
	output004 := readFile("../test/output/output004.txt")
	output005 := readFile("../test/output/output005.txt")
	output006 := readFile("../test/output/output006.txt")
	output007 := readFile("../test/output/output007.txt")
	output008 := readFile("../test/output/output008.txt")
	//output009 := readFile("../test/output/output009.txt")
	output010 := readFile("../test/output/output010.txt")
	output011 := readFile("../test/output/output011.txt")
	output012 := readFile("../test/output/output012.txt")
	type args struct {
		messages []string
	}
	tests := []struct {
		name string
		args args
		want string
	}{
		{
			name: "[T0]",
			args: args{messages: input000},
			want: orderResponse(output000),
		},
		{
			name: "[T1]",
			args: args{messages: input001},
			want: orderResponse(output001),
		},
		{
			name: "[T2]",
			args: args{messages: input002},
			want: orderResponse(output002),
		},
		{
			name: "[T3]",
			args: args{messages: input003},
			want: orderResponse(output003),
		},
		{
			name: "[T4]",
			args: args{messages: input004},
			want: orderResponse(output004),
		},
		{
			name: "[T5]",
			args: args{messages: input005},
			want: orderResponse(output005),
		},
		{
			name: "[T6]",
			args: args{messages: input006},
			want: orderResponse(output006),
		},
		{
			name: "[T7]",
			args: args{messages: input007},
			want: orderResponse(output007),
		},
		{
			name: "[T8]",
			args: args{messages: input008},
			want: orderResponse(output008),
		},
		// {
		// 	name: "[T9]",
		// 	args: args{messages: input009},
		// 	want: orderResponse(output009),
		// },
		{
			name: "[T10]",
			args: args{messages: input010},
			want: orderResponse(output010),
		},
		{
			name: "[T11]",
			args: args{messages: input011},
			want: orderResponse(output011),
		},
		{
			name: "[T12]",
			args: args{messages: input012},
			want: orderResponse(output012),
		},
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			if got := ProcessMessages(tt.args.messages); got != tt.want {
				t.Errorf("ProcessMessages() = %v, want %v", got, tt.want)
			}
		})
	}
}

func readFile(path string) (txtlines []string) {
	file, err := os.Open(path)

	if err != nil {
		log.Fatalf("failed opening file: %s", err)
	}

	scanner := bufio.NewScanner(file)
	scanner.Split(bufio.ScanLines)

	for scanner.Scan() {
		txtlines = append(txtlines, scanner.Text())
	}

	file.Close()
	return
}

func orderResponse(responses []string) string {
	proposals := []string{}
	for _, response := range responses {
		ordered := strings.Split(response, ",")
		sort.Strings(ordered)
		proposals = append(proposals, ordered...)
	}
	return strings.Join(proposals, ",")
}
