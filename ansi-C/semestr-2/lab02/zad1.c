//Napisać program, który wczytuje tekst aż do EOF, a następnie drukuje bitowe obrazy wczytanych znaków.

#include <stdio.h>
#include <stdlib.h>

void bin(int x) {
  int tab[8];
  int i = 0;
  
  while(x != 0)  {
    tab[i] = x % 2;
    x = x / 2;
    i++;
  }
  
  while(i > 0)  {
    i--;
    printf("%i", tab[i]);
  }
  
  printf("\n");
  
}

int main() {
  char z;
  z = getchar();
  
  while(z != EOF) {
    if(z != '\n') bin(z);
      z = getchar();

    if(z == '\n')
      exit(0);
  }
}
