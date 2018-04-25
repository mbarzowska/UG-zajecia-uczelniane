// Napisać makrodefinicję PETLA, której wywołanie
// PETLA(komenda1, warunek, komenda2)
// działa tak, jak na schemacie obok (pętla while ,,zrośnięta'' z pętlą do...while).
// Na przykład wywołanie:
// a=0; b=0; i=0;
// PETLA(a++, i<5, b++; i++);
// powinno przypisywać zmiennej a wartość 6, a zmiennej b wartość 5.

#include <stdio.h>
#define PETLA(komenda1, warunek, komenda2) komenda1; while(warunek){komenda2; komenda1;}
int main()
{
  int a=0, b=0, i=0;
  PETLA(a++, i<5, b++; i++);
  printf("%i\n", a);
  printf("%i\n", b);
  return 0;
}
