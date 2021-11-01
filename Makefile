CONTEST = 
PROBLEM = 

usage:
	@echo "usage: make copy CONTEST=<number> PROBLEM=<a char>\n\te.g.: make copy CONTEST=225 PROBLEM=D"

copy:
	cat "c${CONTEST}/${PROBLEM}.cs" \
		| perl -lpe 's{\b static \s void \s Run \b}{static void Main}msx' \
		| pbcopy

