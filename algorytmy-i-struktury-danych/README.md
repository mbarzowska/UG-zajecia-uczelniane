# Algorytmy i Struktury Danych, laboratorium 2016/2017

:older_man: Prowadzący: [dr Paweł Żyliński](https://inf.ug.edu.pl/~zylinski/)

:older_man: Prowadzący: [dr Maciej Dziemiańczuk](https://inf.ug.edu.pl/~mdziemia/)


## [Binary Search trees](https://github.com/mbarzowska/UG-zajecia-uczelniane/blob/master/Algorytmy-i-Struktury-Danych/BSTree.c)
```$xslt
Zaimplementuj strukturę danych wraz z operacjami WSTAW,SZUKAJ, USUŃ, DRUKUJ, która realizuje koncepcję drzewa 
wyszukiwań binarnych przechowującego liczby całkowite. Przyjąć, że do drzewa wstawiane są liczby o różnych 
wartościach, a wypisanie (DRUKUJ) wartości węzłów odbywa się w porządku in-order.
```


## [heapsort](https://github.com/mbarzowska/UG-zajecia-uczelniane/tree/master/Algorytmy-i-Struktury-Danych/heapsort)


## [lists](https://github.com/mbarzowska/UG-zajecia-uczelniane/blob/master/Algorytmy-i-Struktury-Danych/lists.c)
```$xslt
- Zaimplementuj strukturę listy dowiązaniowej (bez wartownika), której elementami są słowa (ciągi znaków), oraz 
operacje WSTAW(s, L) (wstawia s na początek listy L), DRUKUJ(L) (wypisuje elementu listy L), SZUKAJ(s, L) 
(zwraca wskaźnik na element s, o ile taki element znajduje się na liście L, w przeciwnym wypadku zwraca NULL), 
USUŃ(s, L) (usuwa element s z listy L, o ile znajduje się on na liście L) oraz KASUJ(L) (kasuje wszystkie 
elementy z listy L).
- Operacja BEZPOWTÓRZEŃ(L) polega na otrzymaniu z listy L jej kopii, ale bez powtarzających się elementów; 
lista L nie przestaje istnieć. Zaimplementuj operację BEZPOWTÓRZEŃ (L) (zwraca wskaźnik na listę).
- Mając dwie rozłączne listy L1 i L2, operacja SCAL(L1, L2) polega na otrzymaniu listy L3, której elementami 
są elementy z L1∪L2, tzn. s należy do L3 wtedy i tylko wtedy, gdy s należy do L1 lub s należy do L2; 
listy L1 i L2 stają się puste. Zaimplementuj operację SCAL(L1, L2) (zwraca wskaźnik na wynikową listę L3).
```


## [Longest Common Subsequence](https://github.com/mbarzowska/UG-zajecia-uczelniane/tree/master/Algorytmy-i-Struktury-Danych/LCS)
```$xslt
Zaimplementuj algorytm wyszukiwania najdluzszego wspolnego podciagu metoda programowania dynamicznego.
WEJSCIE:
- dwa napisy A i B dowolnej dlugosci (maksymalnie 1000 znakow) podawane ze standardowego wejscia
WYJSCIE:
- dlugosc najdluzszego wspolnego podciagu A i B oraz jeden najdluzszy wspolny podciag.
```


## [quicksort](https://github.com/mbarzowska/UG-zajecia-uczelniane/blob/master/Algorytmy-i-Struktury-Danych/quicksort.c)
```$xslt
- Zaimplementuj algorytm sortowania szybkiego omówiony na wykładzie.
- Zmierz i porównaj czasy działania sortowania dla dwóch rodzajów danych: danelosowe oraz dane skrajnie 
niekorzystne (np. liczby uprządkowane rosnąco).

Testy (pomiary czasu) powinny być wykonane dla różnych wielkości tablicy, np. 100 elementów, 500 elementów, 
1000 elementów i 2500 elementów.
Ponadto w przypadku danych losowych należy wziąć średni czas (średnia arytmetyczna) z odpowiednio dużej próbki 
(np. 100, 500, 1000, 2500 losowań, odpowiednio, dla tablicy 100-, 500-, 1000-i 2500-elementowej).

Specyfikacja wejścia/wyjścia tylko w przypadku realizacji pierwszej części zadania.
Wejście: Liczby (całkowite) zapisane w kolejnych wierszach pliku tekstowego.
Wyjście: Posortowane liczby z pliku wejściowego zapisane w kolejnych wierszach pliku wyjściowego.
```


## [radix sort](https://github.com/mbarzowska/UG-zajecia-uczelniane/tree/master/Algorytmy-i-Struktury-Danych/radix_sort)
```$xslt
Napisz program sortujący napisy (ciągi liter/cyfr) różnej długości (zajmujące różne ilości pamięci), stosując 
sortowanie pozycyjne (od ostatniego znaku do pierwszego), gdzie sortowanie według kolejnych znaków 
(nie rozróżniając dużych i małych liter) ma być wykonane sortowaniem przez zliczanie.
```


## [Red-Black trees](https://github.com/mbarzowska/UG-zajecia-uczelniane/blob/master/Algorytmy-i-Struktury-Danych/RBTree.c)
```$xslt
Zaimplementuj strukturę drzewa czerwono-czarnego, której kluczami są liczby całkowite wraz z operacjami:
- SZUKAJ(T, x) - wyszukuje element o kluczu x w drzewie T
- DRUKUJ(T) - wypisuje elementy drzewa T metodą inorder
- DODAJ(T, x) - dodawanie elementu o kluczu x do drzewa T (wszystkie trzy przypadki)
```


## [selection_sort](https://github.com/mbarzowska/UG-zajecia-uczelniane/blob/master/Algorytmy-i-Struktury-Danych/selection_sort.c)
```$xslt
Zaimplementuj algorytm wyznaczania k-tego co do wielkości elementu tablicy przechowującej n różnych liczb 
całkowitych, 1 ≤ k ≤ n, oparty na metodzie „dziel i zwyciężaj”, a następnie przetestuj eksperymentalnie 
jego złożoność obliczeniową.

Zadanie zrobione z użyciem tablicy z przykładu zaprezentowanego na wykładzie
{3,50,60,63,11,4,5,85,70,99,61,101,62,19,22,10,30,1,100,9,82,21,40,71,20,80,81,79}
```
