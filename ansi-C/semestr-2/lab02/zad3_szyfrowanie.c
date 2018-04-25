// Tzw. ,,blokowe'' algorytmy szyfrujące kodują jednocześnie całe bloki tekstu, znacznie dłuższe niż pojedyncze litery.
// Napisać program szyfrujący oraz współpracujący z nim program odszyfrowujący, działające w następujący sposób:
//    wczytany tekst jest dzielony na grupy po 4 kolejne znaki; każda taka grupa szyfrowana jest oddzielnie;
//    taką grupę traktuje się jak pojedynczą liczbę całkowitą; poddaje się ją jakiejś różnowartościowej funkcji matematycznej (,,kluczowi'' szyfru); np.
//        n → −n     albo
//        n → (n+k)%232   gdzie k jest ustaloną liczbą całkowitą;
//    otrzymany ciąg liczb przesyła się adresatowi;
//    każdą otrzymaną liczbę adresat odszyfrowuje funkcją odwrotną do klucza, a następnie rozbija na 4 znaki; te znaki drukuje.
// Szyfrowanie:

#include<stdio.h>
#include<stdlib.h>

#define PAKOWANIE 4
// definiuje pakowanie 4 znaków do jednej liczby

int klucz(int n) {
  return -n;
} // jak szyfrować liczbę

int szyfruj(int buf[PAKOWANIE]) {
  int i;
  int j = 0;
  
  for (i = 0; i < PAKOWANIE; i++)
    j = (j<<8)|(buf[i]&255);
  return j;
}

int main() {
  int ile = 0;
  int buf[PAKOWANIE];
  buf[ile] = (int)getchar();

  while (buf[ile] != EOF) {
    ile++;
    
    if (ile == PAKOWANIE) {
      printf(" %11i\n", klucz(szyfruj(buf)));
      ile = 0;
    }
    buf[ile] = (int)getchar();
  }
 
  if (ile > 0) {
    int i;
    for (i = ile; i < PAKOWANIE; i++)
      buf[i]=' ';
    printf(" %10i\n", klucz(szyfruj(buf)));
  } // spacje
}
