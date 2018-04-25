#include<stdio.h>

#define PAKOWANIE 4
  // po ile znakow pakowane do jednej liczby

int klucz(int n) {
  return -n;
} // odszyfrowywanie liczby

void odszyfruj(int n) {
    // wydruk wszystkich znakow z paczki
  int buf[PAKOWANIE];
  for (int i=0; i<PAKOWANIE; i++) {
    buf[i] = n&255; n >>= 8;
  }
  for (int i=PAKOWANIE-1; i>=0; i--)
    printf("%c", (char)buf[i]);
}

int main () {
  int n, wprowadzenie;
  wprowadzenie = scanf("%i", &n);
  while (wprowadzenie == 1) {
    odszyfruj(klucz(n));
    wprowadzenie = scanf("%i", &n);
  }
  printf("\n");
}

