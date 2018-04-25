// Napisać programy testujące działanie operatorów z efektami ubocznymi w różnych sytuacjach, od których może zależeć wynik działania.

#include<stdio.h>

int main() {
  int x, y;
   
  x = 5; y = 3;
  printf("x = 5, y = 3 \n");

  y = (x += 2);
  printf("y wynosi: %d\n", y);

  x = (y %= 3) + (y %= 4);
  printf("x wynosi: %d\n", x);

  y = (y %= 3) + (y %= 4) + (x += 2);
  printf("y wynosi: %d\n", y);
}
