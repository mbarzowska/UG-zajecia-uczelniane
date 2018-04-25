#include<stdio.h>
#include<string.h>

int i, j, m, n, c[1000][1000];
char x[1000], y[1000], b[1000][1000];

int ile = 0;

void LCS_LENGTH(char *x, char *y) {
  m = strlen(x);
  n = strlen(y);
  for(i = 0; i <= m; i++)
    c[i][0] = 0;
  for(j = 0; j <= n; j++)
    c[0][j] = 0;
  for(i = 1; i <= m; i++) {
    for(j = 1; j <= n; j++) {
      if(x[i-1] == y[j-1]) {         
        c[i][j] = c[i-1][j-1]+1;
        b[i][j] = '\\';
      }
      else if(c[i-1][j] >= c[i][j-1]) {
        c[i][j] = c[i-1][j];
        b[i][j] = '|';
      }
      else {
        c[i][j] = c[i][j-1];
        b[i][j] = '-';
      }
    }
  }
}

void PRINT_LCS(char *x, int i, int j) {    
  if(i == 0 || j == 0)
    return;
  if(b[i][j] == '\\') {
    PRINT_LCS(x, i-1, j-1);
    printf("%c", x[i-1]); ile++;
  }
  else if(b[i][j] == '|')
    PRINT_LCS(x, i-1, j);
  else
    PRINT_LCS(x, i, j-1);
}

int main(int argc, char** argv) {
 char *x = argv[1];
 char *y = argv[2];
  printf("Najdłuższy wspólny podciąg wprowadzonych ciągów to: ");
  LCS_LENGTH(x, y);
  PRINT_LCS(x, m, n);   printf("\n");
  printf("Długość najdłuższego wspólnego podciągu to: %d\n\n", (ile));
  /* ile-1, ponieważ fgets pobiera także znak \n, który jest potem analizowany */
  return 0;
}
