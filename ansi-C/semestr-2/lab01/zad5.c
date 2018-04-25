// Co wydrukuje następujący fragment programu:
// Należy zwrócić uwagę, że w warunku powyższej instrukcji if występuje koniunkcja bitowa & a nie koniunkcja logiczna &&.

#include<stdio.h>

int main() {
  int a = 2, b = 4;

  if ((a = 2) & (b = 4))
    printf("TAK\n");
  else
    printf("NIE\n");
}

// Ten program wydrukuje "NIE".
// W przypadku koniunkcji logicznej && program wydrukowałby "TAK".
