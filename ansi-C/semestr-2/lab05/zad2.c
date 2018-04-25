#include<stdlib.h>
#include<stdio.h>
#include<string.h>
#include<ctype.h>

#define IMIE_MAX 15
#define NAZW_MAX 15
#define IL_OSOB 10

typedef struct {
  char imie[IMIE_MAX+1];
  char nazwisko[NAZW_MAX+1];
  int pensja;
} osoba;

osoba spis[IL_OSOB];

//=======================================================

void  utworz_spis(void)
{
  FILE* baza =
    fopen("zad2_nieposortowane.txt", "r");
  if (baza == NULL) printf("\n ZLE\n\n");
  int i;
  for (i=0; i<IL_OSOB; i++) {
  	fscanf(baza, "%s", spis[i].imie);
    fscanf(baza, "%s", spis[i].nazwisko);
    fscanf(baza, "%i", &spis[i].pensja);
  }
  fclose(baza);
}
int my_compare (const void * a, const void * b)
{
    osoba *osoba_a, *osoba_b;
    osoba_a = (osoba*)a;
    osoba_b = (osoba*)b;

    return strcmp(osoba_a->nazwisko, osoba_b->nazwisko);
}
//=======================================================
void  sortuj_spis(void)
{
	qsort(spis, IL_OSOB, sizeof(osoba), my_compare);
	FILE* baza2=fopen("posortowane.txt","w");
	int i;
    for(i=0;i<IL_OSOB;i++)
    {
       fprintf(baza2, "%20s	", spis[i].imie);
       fprintf(baza2, "%20s	", spis[i].nazwisko);
       fprintf(baza2, "%20i\n", spis[i].pensja);
    }
    fclose(baza2);
}
//=======================================================

int  znajdz_nazwisko (char na[NAZW_MAX+1], char im[IMIE_MAX+1], int *p)
{
	osoba * a=(osoba*)bsearch(na, spis, IL_OSOB, sizeof(osoba), my_compare);
	if(a)
	{
		strcpy(im,a->imie);
    	*p=a->pensja;
    	return 1;
    }
	else
    return 0;
    	
	
	
}

//=======================================================

int  znajdz_imie (char im[NAZW_MAX+1], char na[IMIE_MAX+1], int *p)
{
	
  	int i;
	for(i=0; i<IL_OSOB; i++)
	{
		if(strcmp(spis[i].imie,im) == 0)
		{
			strcpy(na,spis[i].nazwisko);
			*p = spis[i].pensja;
			return 1;
		}
	}

	return 0;
  }

//=======================================================

int main ()
{
  char odpowiedz, im[NAZW_MAX+1], na[IMIE_MAX+1];
  int p;

  utworz_spis(); sortuj_spis();

  do {
    printf(
     "\n Znalezc wg imienia (I), nazwiska (N), czy zakonczyc (Q)? "
    );
    do { odpowiedz = getchar(); }
    while (isspace(odpowiedz));
    odpowiedz = tolower(odpowiedz);
    switch (odpowiedz) {
    case 'i' :
      printf("\n szukane imie: ");
      scanf("%s", im);
      if (znajdz_imie(im, na, &p))
	printf(" IMIE: %s, NAZWISKO: %s, PENSJA: %i\n", im, na, p);
      else  printf(" nie ma imienia %s\n", im);
      break;
    case 'n' :
      printf("\n szukane nazwisko: ");
      scanf("%s", na);
      if (znajdz_nazwisko(na, im, &p))
	printf(" IMIE: %s, NAZWISKO: %s, PENSJA: %i\n", im, na, p);
      else  printf(" nie ma nazwiska %s\n", im);
      break;
    case 'q' : break;
    default :
      printf(" Poprawne odpowiedzi: N, I, Q.\n");
    }
  }  while (tolower(odpowiedz) != 'q');

  printf("\n DZIEKUJE.\n\n");
}
