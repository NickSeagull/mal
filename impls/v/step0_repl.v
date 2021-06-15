import readline

fn read(input string) string {
	return input
}

fn eval(input string) string {
	return input
}

fn prnt(input string) string {
	return input
}

fn rep(input string) string {
	read_result := read(input)
	eval_result := eval(read_result)
	return prnt(eval_result)
}

fn main() {
	for {
		input := readline.read_line('user> ') or {
			println('\n\nGoodbye!')
			exit(0)
		}
		println(rep(input))
	}
}
