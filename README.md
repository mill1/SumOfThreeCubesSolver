# SumOfThreeCubesSolver
Small C# console application that lists primitive solutions for the sums of three cubes: x³ + y³ + z³ = n

See https://en.wikipedia.org/wiki/Sums_of_three_cubes and https://en.wikipedia.org/wiki/Diophantine_equation for details.

The program generates a text file containing the solutions and is set up in order to easily add other solvers.

Inspired by the repo https://github.com/fleschutz/LSS of Markus Fleschutz.

## Command line arguments
The program makes use of the following optional arguments:
* "solver", string, determines which type of solver to use. Default value: "brute force solver"
* "start value", int, value from which the three elements will be cubed. E.g. -5 means that the first combination to check will be -5³ + -5³ + -5³. Default: -10
* "end value", int, value up to which the three elements will be cubed. Default: 10
* "process annulling solutions", bool, skip combinations where the sum of two of the three elements is zero. F.i: -9³ + 2³ + 9³. Default: false
* "print from", int, value from which the solutions will be printed. Default: sum of three cubes of "start value" (e.g. -375 in case of -5)
* "print until", int, value up to which the solutions will be printed. Default: sum of three cubes of "end value"
* "print no solutions", skip printing values for which there are no solutions. Default: true ***1**
* "path", string, output folder for the text file. Default: _MyDocuments_ folder

Command using all arguments:

`
SumOfThreeCubesSolver.exe "solver:brute force solver" "start value:-5" "end value:5" "process annulling solutions:false" "print from:0" "print until:1000" "print no solutions:true" "path:C:\Docs"
`
## Release notes
None


***1**

There are two reasons a solution cannot be found:
1. No solution exists for n equals 4 or 5 modulo 9
2. The range defined by "start value" and "end value" is too small to find (a) solution(s).
