#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>

#define MAX_DL_IM 11
#define MAX_DL_NA 17

#define IL_OSOB 10000

typedef struct {
  char imie[MAX_DL_IM];
  char nazwisko[MAX_DL_NA];
  int pensja;
} osoba;

osoba spis[IL_OSOB];

int czytanie(char *nazwapliku) {
  int i=0;
  char znak;
  FILE* baza = fopen(nazwapliku, "r");
  if(baza==NULL) printf("\n ZLE\n\n");
  while((znak=getc(baza)) != EOF ) {
    if(znak == '\n')
      i++;
  }
  fclose(baza);
  return i;
}

void utworz_spis(char *nazwapliku, int ile) {
  FILE* baza = fopen(nazwapliku, "r");
  if(baza==NULL) printf("\n ZLE\n\n");
  for(int i=0;i<ile;i++) {
    fscanf(baza, "%s", spis[i].imie);
    fscanf(baza, "%s", spis[i].nazwisko);
    fscanf(baza, "%i", &spis[i].pensja);
  }
  fclose(baza);
}

int porownanie(const void  * a, const void * b) {
  osoba *osoba_a, *osoba_b;
  osoba_a=(osoba*) a;
  osoba_b=(osoba*) b;
  return strcmp(osoba_a->nazwisko, osoba_b->nazwisko);
}

void sortuj_spis(int ile) {
  /* sortuje  spis  alfabetycznie wg nazwisk,
  a w przypadku rownych nazwisk wg imion */
     
  clock_t  pocz = clock();
  qsort(spis, ile, sizeof(osoba), porownanie);
  clock_t  koniec = clock();
     
  printf(" Czas wykonania: %lf sek.\n\n",(double)(koniec-pocz) / CLOCKS_PER_SEC);
     
  FILE *baza = fopen("posortowane.txt", "w");
  int i;
  for(i = 0; i < ile ; i++) {
    fprintf(baza, "%20s", spis[i].imie);
    fprintf(baza, "%20s", spis[i].nazwisko);
    fprintf(baza, "%20i\n", spis[i].pensja);
  }
 fclose(baza);
}

int main (int arg_num, char* arg[]) { 
  int ile;
  if (arg_num == 3) {
    ile = czytanie(arg[1]);
    if(ile > 0) {
      utworz_spis(arg[1], ile);
      sortuj_spis(ile);
    }
    printf("%i", ile);
  }
  else
    printf("\n Niepoprawne wywolanie: './porzadkuj baza.txt posortowane.txt'\n\n");
}
