// Eksperymentalnie zbadać ,,ziarno'' w różnych typach rzeczywistych. W tym celu należy wykonać pętlę postaci podobnej do poniższej:
// x = 1.0;
// while (1.0+x > 1.0)
// x = x/2.0;
// dla zmiennej x zadeklarowanej kolejno jako float, double i long double.

#include<stdio.h>

int main() {
  int l = 1;

  printf("Float: \n");
  float f = 1.0;
  while (1.0+f > 1.0) {
    f = f/2.0;
    printf("%i. %f => %e\n", l, f, f);
    l++;
  }

  l = 1;
  printf("Double: \n");
  double d = 1.0;
  while (1.0+d > 1.0) {
    d = d/2.0;
    printf("%i. %f => %e\n", l, d, d);
    l++;
  }

  printf("Long double: \n");
  long double ld = 1.0;
  while (1.0+ld > 1.0) {
    ld = ld/2.0;
    printf("%i. %Lf => %Le\n", l, ld, ld);
    l++;
  }
}
