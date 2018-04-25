// for ( ; (z=getchar())!=EOF; printf("%c",z)); 
// Napisać czysty program, czyli bez efektów ubocznych, działający tak samo jak powyższa pętla for.

#include<stdio.h>
#include<stdlib.h>

int main() {
  char z;
  z = getchar();

  while(z != EOF) {
    printf("%c", z);
    z=getchar();
    exit(0);
  }	
}
