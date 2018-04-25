# Architektura komputerów, laboratorium 2016/2017
ASMx86


## [Zadanie 1 / Task 1](01_longestString.c)
PL: Znaleźć długość najdłuższego ciągu jedynek w rozwinięciu binarnym liczby x. Na wyjściu x2 jako ta długość.

ENG: Find the length of the longest 1's series in binary representation of x. Output: x2, which represents the length.


## [Zadanie 2 / Task 2](02_findingRegularExpression.c)
PL: Uzupełnić program tak, aby po jego uruchomieniu na ekranie terminala wypisana została pozycja i długość pierwszego dopasowania wyrażenia regularnego [pq][^a]+a w łańcuchu s. Np. w przypadku, gdy s = "aqr  b qabxx xryc pqr", to efektem działania programu powinno być wyprowadzenie na ekran łańcucha 1,8."

ENG: Complete the program so that after its launch, the position and length of the first match of the regular expression [pq][^a]+a in the chain s is printed on the terminal screen. For example, if s = "aqr b qabxx xryc pqr", then the program should print 1,8 on the screen. "


## [Zadanie 3](03_recursiveFunction.s)
![tresc](https://i.imgur.com/eAgOV9J.jpg)


## [Zadanie 4 / Task 4](04.s)
PL: Napisać program pobierający z linii zleceń dwa argumenty x i y tak, aby na ekranie terminala po jego uruchomieniu wypisane zostało po sobie x ostatnich słów z łańcucha y (słowa są to ciągi znaków alfanumerycznych oddzielonych od siebie dowolną liczbą spacji). Np. w przypadku gdy x = ”7”, a y = ”axab” efektem działania programu powinno być: ababababababab. O wszystkich wprowadzanych/wyprowadzanych liczbach można zakładać, że są z zakresu 0-999.

Ułożenie argumentów:
x - liczba; y - ciąg znaków, musi być w "", bo jest spacja

Uruchomienie programu:
./a.out *liczba* "wyrażenie"

ENG: Write a program that retrieves two arguments x and y from the command line, so that on the screen of the terminal after its run, the last x words of the string y are printed (words are sequences of alphanumeric characters separated by any number of spaces). For example, if x = "7" and y = "axab" the effect of the program should be: ababababababab. All numbers entered / output can be assumed to be in the 0-999 range.

Arranging arguments:
x - number; y - string

Starting the program:
./a.out *number* "expression"
