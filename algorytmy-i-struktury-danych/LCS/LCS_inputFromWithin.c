#include<stdio.h>
#include<string.h>

int i, j, m, n, c[1000][1000];
char x[1000], y[1000], b[1000][1000];

int ile = 0;

void LCS_LENGTH(char* x, char* y) {
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

void PRINT_LCS(int i, int j) {         
  if(i == 0 || j == 0)
    return;
  if(b[i][j] == '\\') {
    PRINT_LCS(i-1, j-1);
    printf("%c", x[i-1]); ile++;
  }
  else if(b[i][j] == '|')
    PRINT_LCS(i-1, j);
  else
    PRINT_LCS(i, j-1);
}

int main() {
  printf("\nWprowadź pierwszy ciąg: ");
// scanf("%s",x);
  fgets(x, 10000, stdin);
  if(x[0] == '\n') {
    printf("Nie wpisałeś poprawych danych. Program zakończy działanie.\n\n");
    return 0;
  }
  printf("Wprowadź drugi ciąg: ");
  fgets(y, 10000, stdin);
  if(y[0] == '\n') {
    printf("Nie wpisałeś poprawych danych. Program zakończy działanie.\n\n");
    return 0;
  }

  printf("Najdłuższy wspólny podciąg wprowadzonych ciągów to: ");
  LCS_LENGTH(x,y);
  PRINT_LCS(m,n);
  printf("Długość najdłuższego wspólnego podciągu to: %d\n\n", (ile-1));
  /* ile-1, ponieważ fgets pobiera także znak \n, który jest potem analizowany */
  return 0;
}
