// Napisać program, który bada poznane typy liczbowe w C, tzn. ustala:
// * ilość bajtów, przeznaczonych na zmienną (operator sizeof),
// * największą i najmniejszą wartość liczby,
// * ,,ziarno'', czyli najmniejszą taką liczbę x, że 1.0+x≠1.0 ,
// * ,,precyzję'', czyli maksymalną liczbę cyfr dziesiętnych po kropce.

#include<stdio.h>
#include<float.h>
#include<limits.h>

int main() {
  printf("|%10s|%20s|%20s|%15s|%15s|%15s|\n", "typ", "wartosc min", "wartoc max", "ziarno", "precyzja", "we/wy");
  printf("|----------|--------------------|--------------------|---------------|---------------|---------------|\n");
  printf("|%10s|%20i|%20i|%15c|%15c|%15s|\n", "short", SHRT_MIN, SHRT_MAX, ' ', ' ', "i");
  printf("|%10s|%20i|%20i|%15c|%15c|%15s|\n", "int", INT_MIN, INT_MAX, ' ', ' ', "i");
  printf("|%10s|%20li|%20li|%15c|%15c|%15s|\n", "long", LONG_MIN, LONG_MAX, ' ', ' ', "li");
  printf("|%10s|%20lli|%20lli|%15c|%15c|%15s|\n", "long long", LLONG_MIN, LLONG_MAX, ' ', ' ', "lli");
  printf("|%10s|%20e|%20e|%15e|%15i|%15s|\n", "float", FLT_MIN, FLT_MAX, FLT_EPSILON, FLT_DIG, "e, f");
  printf("|%10s|%20le|%20le|%15e|%15i|%15s|\n", "double", DBL_MIN, DBL_MAX, DBL_EPSILON, DBL_DIG, "le, lf");
  printf("|%10s|%20Le|%20Le|%15Le|%15i|%15s|\n", "long double", LDBL_MIN, LDBL_MAX, LDBL_EPSILON, LDBL_DIG, "Le, Lf");

  printf("\nLiczba zajmowanych bajtów:\n");
  printf("short:\t\t%li\n", sizeof(short));
  printf("int:\t\t%li\n", sizeof(int));
  printf("long:\t\t%li\n", sizeof(long));
  printf("long long:\t%li\n", sizeof(long long));
  printf("float:\t\t%li\n", sizeof(float));
  printf("double:\t\t%li\n", sizeof(double));
  printf("long double:\t%li\n", sizeof(long double));
}
