// Napisałem program, który
// wczytuje dzień (między 1 a 31), miesiąc (między 1 a 12) i rok (liczbę całkowitą dodatnią, niewiększą niż 223−1),
// wszystkie trzy dane pakuje do jednej 32-bitowej liczby całkowitej, jak na poniższym rysunku
// drukuje tą liczbę.
// Program wydrukował liczbę 1032225. Co to była za data?
// Napisać program, który potrafi odczytać datę z każdej liczby, zestawionej w podany wyżej sposób.

#include <stdio.h>

int main() {
  int x, co;
  
  printf("Co chcesz zrobić? \nPolicz zadany przykład - 1 \nPolicz dowolną liczbę - 2\n");
  scanf("%d", &co);
  
  switch(co) {
    case 1:	x = 1032225;
        	licz(x);
		break;
    case 2:
		printf("Podaj liczbe: \n");
		scanf("%d",&x);
		licz(x);
		break;
  }
}

int licz(int x) {
  int day = (x & 0x0000001F);
  int month = (x & 0x000001E0) >> 5;
  int year = (x & 0xFFFFFE00) >> 9;
  printf("Dzien = %d, Miesiac = %d, Rok = %d\n", day, month, year);
}
