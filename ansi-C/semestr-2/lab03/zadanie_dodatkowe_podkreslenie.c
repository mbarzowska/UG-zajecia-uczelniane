// Napisać funkcję która zamienia zapis liczb wstawiając znak _ co trzy cyfry od końca
// Przykład 123456789 => 123_456_789
// Lab3 Zad. dodatkowe

#include<stdio.h>

void underscore(int n) {
  int n2 = 0;
  int scale = 1;
  
  if (n < 0) {
    printf ("-");
    n = -n;
  }

  while (n >= 1000) {
    n2 = n2 + scale * (n % 1000);
    n /= 1000;
    scale *= 1000;
  }

  printf ("%d", n);
  while (scale != 1) {
    scale /= 1000;
    n = n2 / scale;
    n2 = n2  % scale;
    printf ("_%03d", n);
    printf("\n");
  }
}

int main() {
  int number;
  scanf("%d", &number);
  underscore(number);
}
