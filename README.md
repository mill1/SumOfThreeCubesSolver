# SumOfThreeCubesSolver
Small C# console application that lists primitive solutions for the sums of three cubes: x³ + y³ + z³ = n

See https://en.wikipedia.org/wiki/Sums_of_three_cubes and https://en.wikipedia.org/wiki/Diophantine_equation for details.

The program generates a text file containing the solutions and is set up in order to easily add other solvers.

Inspired by the repo https://github.com/fleschutz/LSS of Markus Fleschutz.

## Command line arguments
The program makes use of the following optional arguments:
* "solver", string, determines which type of solver to use. Default value: "brute force solver"
* "start value", int, value from which the three elements will be cubed. E.g. -5 means that the first combination to check will be -5³ + -5³ + -5³. Default: -100
* "end value", int, value up to which the three elements will be cubed. Default: 100
* "process annulling solutions", bool, skip combinations where the sum of two of the three uncubed elements is zero. F.i: -9³ + 2³ + 9³. Default: false
* "print from", int, value from which the solutions will be printed. Default: sum of three cubes of "start value" (e.g. -375 in case of -5)
* "print until", int, value up to which the solutions will be printed. Default: sum of three cubes of "end value"
* "print no solutions", skip printing values for which there are no solutions. Default: true ***1**
* "text warning threshold", int, number of generated output lines from which a warning will be shown before proceeding. Default: 1000000 (file size:~30Mb) ***2**
* "path", string, output folder for the text file. Default: _MyDocuments_ folder

Command using all arguments:

`
SumOfThreeCubesSolver.exe "solver:brute force solver" "start value:-100" "end value:100" "process annulling solutions:false" "print from:-1000" "print until:1000" "print no solutions:true" "text warning threshold:100000" "path:C:\Docs"
`
## Release notes
None


***1**

There are two reasons a solution cannot be found:
1. No solution exists for n equals 4 or 5 modulo 9
2. The range defined by "start value" and "end value" is too small to find (a) solution(s).

***2**

Next combination of arguments can result a large number of lines being generated in the output file:

"print no solutions:true", "print from": missing, "print until": missing

This combination leeds to a very large file when the arguments "start value" and "end value" cover a significant range. For example: "start value:-500", "end value:500" will result in 500³ + 500³ = 250.000.000 lines in the output file.
