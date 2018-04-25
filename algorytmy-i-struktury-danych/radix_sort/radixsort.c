#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<ctype.h>                       

#define dlugosc_slowa 50
#define ilosc_slow 60                   
#define zakres 128                      

char **A;                                     
char **B;                                         
char **pom;

void CountingSort (char **A, char **B, int ilosc, int pozycja) {
  int i, j;
  int C[1000];                                    

  for (i = 0; i <= zakres; i++)                  
    C[i] = 0;                                      
  for (j = 1; j <= ilosc; j++)                    
    C[A[j][pozycja]] += 1;                       
  for (i = 1; i <= zakres; i++)       
    C[i] = C[i] + C[i-1];                        
  for (j = ilosc; j > 0; j--) {              
    B[C[A[j][pozycja]]] = A[j];                   
    C[A[j][pozycja]] = C[A[j][pozycja]] - 1;      
    }
}

void RadixSort (char **A, char **B, int najdluzsze) {
  int i;

  for (i = najdluzsze - 1; i >= 0; i--) {         
    CountingSort (A, B, ilosc_slow, i);             
    pom = A;
    A = B;
    B = pom;                                     
  }
}

void czytaj_z_pliku (char **tablica, int ilosc) {  
  FILE *fp  = fopen("input.txt", "r");             
  if (fp == NULL) {
    printf("Nie można znaleźć pliku wejściowego input.txt!\n");
    exit(0);
  }

  char slowo [dlugosc_slowa];                       
  int i, j;                                         

  for (i = 1; i <= ilosc; i++) {                                         
    fscanf(fp, "%s", slowo);                      

    for (j = 0; j < strlen(slowo); j++)         
      slowo[j] = tolower(slowo[j]);             

    tablica[i] = (char*) malloc(sizeof(char) * dlugosc_slowa);  
    strcpy(tablica[i], slowo);                     
  }
  fclose(fp);                                     
}

void drukuj_posortowane_do_pliku (char **tablica, int ilosc) {
  FILE *fp = fopen("output.txt", "w");

  int i;

  for (i = 1; i <= ilosc; i++)                      
    fprintf(fp, "%s \n", tablica[i]);              

  fclose(fp);
}

int najwieksza_dlugosc_napisu (char **tablica, int ilosc) {
  int i, max = 0;

  for (i = 1; i <= ilosc; i++) {                    
    if (strlen(tablica[i]) > max)                   
      max = strlen(tablica[i]);                     
  }
  return max;                                       
}

void wyrownaj_dlugosc_napisow (char **tablica, int ilosc, int najdluzsze) {
  int i, j;

  for (i = 1; i <= ilosc; i++) {                   
    for (j = 0; j <= najdluzsze; j++) {             
      if ((96 > (int)tablica[i][j] && (int)tablica[i][j] < 123))  
        tablica[i][j] = 0;                         
    }
  }
}

int main () {
  A = (char**) malloc(ilosc_slow * sizeof(char*));  
  B = (char**) malloc(ilosc_slow * sizeof(char*));  
  pom = (char**) malloc(ilosc_slow * sizeof(char*));

  int najdluzsze;

  czytaj_z_pliku (A, ilosc_slow);                   

  najdluzsze = najwieksza_dlugosc_napisu (A, ilosc_slow);
  wyrownaj_dlugosc_napisow (A, ilosc_slow, najdluzsze);

  RadixSort (A, B, najdluzsze);

  drukuj_posortowane_do_pliku (B, ilosc_slow);
  printf("Posortowano zawartość pliku input.txt. Wynik zapisano w pliku output.txt.\n");
}
