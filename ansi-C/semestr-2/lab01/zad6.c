// Sprawdzić i wyjaśnić działanie następujących fragmentów programów:
// a = b = c = d = 0;
// for (i=0; i<N; a[i++]=i);
// for (i=0; i<N; a[++i]=i);
// i=1; while ((i*=2)<N);

#include<stdio.h>

int main() {
  int a, b, c, d;

  a = b = c = d = 0;
  printf("%d %d %d %d \n", a, b, c, d);
  // zmienne a, b, c, d są równe 0

  int  i, N = 5;
  int t[6];
  
  for (i = 0; i < N; t[i++] = i)
    printf("na miejscu %d znajduje się liczba %d \n", t[i], i);
    // i-ty indeks tablicy przyjmuje wartość i oraz zostaje zwiększony o jeden

  for (i = 0; i < N; t[++i] = i)
    printf("na miejscu %d znajduje się liczba %d \n", t[i], i);
    // i-ty indeks tablicy zwiększa się o jeden i przyjmuje wartość i

  i = 1;
  while ((i *= 2) < N)
    printf(" %d \n", i);
    // dopóki i < N, i jest mnożone razy 2 i drukowane na ekran
}
