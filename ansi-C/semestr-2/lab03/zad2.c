//W każdym typie liczbowym znaleźć możliwie małe dane, na których wynik jakiegoś działania matematycznego jest już niepoprawny.

#include <stdio.h>

int main()
{
  short shr=32767;
  short shr2=shr+10;
  printf("Short: %i + 10 = %i\n", shr, shr2);

  int in=2000000000;
  int in2=in+1000000000;
  printf("Int: %i + 1000000000 = %i\n", in, in2);

  long lg=9000000000000000000;
  long lg2=lg+1000000000000000000;
  printf("Long: %li + 1000000000000000000 = %li\n", lg, lg2);

  long long llg=9000000000000000000;
  long long llg2=llg+1000000000000000000;
  printf("Long long: %lli + 1000000000000000000 = %lli\n", llg, llg2);

  float fl=0.000001;
  float fl2=fl/2.0;
  printf("Float: %f / 2 = %f\n", fl, fl2);

  double dl=0.000003;
  double dl2=dl/4.0;
  printf("Double: %f / 4 = %f\n", dl, dl2);

  long double ldl=0.00000000000000005;
  long double ldl2=dl/4.0;
  printf("Long double: %Lf / 4 = %Lf\n", ldl, ldl2);
}
