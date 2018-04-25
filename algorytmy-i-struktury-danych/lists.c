#include<stdio.h>
#include<string.h>
#include<stdlib.h>

typedef struct Lista {
  char* slowo;
  struct Lista *next;
  struct Lista *previous;
} lista;

lista *L1 = NULL;
lista *L2 = NULL;
lista *L3 = NULL;

void WSTAW (char* s, lista **L) {
  lista *nowy = (lista*) malloc (sizeof(lista));
  nowy->slowo = s;                             
  nowy->next = *L;                             
  nowy->previous = NULL;
  *L = nowy;
}

void DRUKUJ (lista *wezel) {
  while (wezel != NULL) {                        
    printf("%s ", wezel->slowo);                
    wezel = wezel->next;                       
  }
  printf("\n");
}

void DRUKUJ_dla_szukania (lista *wezel) {       
  if (wezel != NULL) {
    printf("%s ", wezel->slowo);                
  }
  printf("\n");
}

lista* SZUKAJ (char* s, lista* wezel) {
  while (wezel!=NULL) {                         
    if (wezel->slowo == s) {                   
      return wezel;                            
    }                                           
    wezel = wezel->next;                        
  }
return NULL;
}

void USUN (char* s, lista** L) {
  lista* previous;
  lista* tmp = *L;

  while (tmp != NULL) {
    if (tmp->slowo == s) {
      if (tmp == *L) {
        *L = tmp->next;
        return;
      } else {
        previous->next = tmp->next;
        free(tmp);
        return;
      }
    } else {
      previous = tmp;
      tmp = tmp->next;
    }
  }
}

void KASUJ (lista** L) {
  lista* tmp;

  while (*L != NULL) {
    tmp = *L;
    *L = (*L)->next;
    if (tmp->slowo)
      free(tmp);
  }
}

void BEZPOWTORZEN (lista* L) {
  lista *wezel1, *wezel2, *tmp;
  wezel1 = L;
  while (wezel1 != NULL && wezel1->next != NULL) {
    wezel2 = wezel1;
    while (wezel2->next != NULL) {
      if (wezel1->slowo == wezel2->next->slowo) {
        tmp = wezel2->next;
        wezel2->next = wezel2->next->next;
        free(tmp);
      } else {
      wezel2 = wezel2->next;
      }
    }
    wezel1 = wezel1->next;
  }
}

void SCAL(lista** lista1, lista** lista2) {
  lista* tmp = (lista*) malloc (sizeof(lista));
  tmp = *lista2;                                                     
  while(tmp != NULL) {                                          
    WSTAW(tmp->slowo, &L3);
    tmp = tmp->next;
  }
  tmp = *lista1;
  while (tmp != NULL) {
    WSTAW (tmp->slowo, &L3);
    tmp = tmp->next;
  }
  *lista1 = NULL;
  *lista2 = NULL;
}


int main () {
  printf ("\n");

  WSTAW ("ajerkoniak", &L1);
  WSTAW ("abażur", &L1);
  WSTAW ("asceza", &L1);
  WSTAW ("abażur", &L1);
  WSTAW ("auto", &L1);
  printf ("Lista L1 to: ");
  DRUKUJ (L1);

  WSTAW ("bakłażan", &L2);
  WSTAW ("bambus", &L2);
  WSTAW ("borsuk", &L2);
  WSTAW ("bakłażan", &L2);
  WSTAW ("bóbr", &L2);
  printf ("Lista L2 to: ");
  DRUKUJ (L2);
  printf ("\n\n");

  printf ("Test funkcji SZUKAJ\n");
  lista* search;
  search = SZUKAJ ("borsuk", L2);
  printf ("Szukaj borsuk: ");
  DRUKUJ_dla_szukania (search);
  printf ("\n\n");

  printf ("Test funkcji USUŃ na liście L1\n");
  printf ("Lista L1: "); DRUKUJ(L1);
  printf ("USUN(\"ajerkoniak\"): "); USUN("ajerkoniak", &L1);
  DRUKUJ(L1);
  printf ("\n\n");

  printf ("Test funkcji KASUJ na liście L1\n");
  printf ("Lista L1: "); DRUKUJ(L1);
  printf ("KASUJ listę L1: "); KASUJ(&L1); printf ("\n");

  DRUKUJ(L1);
  printf ("W poprzedniej linii uruchomiona była funkcja DRUKUJ(L1) efekt to pusta linia, bo lista została usunięta (w kodzie linia 198)\n\n\n");

  printf ("Test funkcji BEZPOWTORZEN na liście L2\n");
  printf ("Lista L2: "); DRUKUJ (L2);
  printf ("Lista L2 bez powtórzeń: "); BEZPOWTORZEN(L2);
  DRUKUJ(L2);
  printf ("\n\n");

  L1 = NULL;
  WSTAW ("ajerkoniak", &L1);
  WSTAW ("abażur", &L1);
  WSTAW ("asceza", &L1);
  WSTAW ("abażur", &L1);
  WSTAW ("auto", &L1);
  printf ("Odbudowanie listy L1 w toku...\n");
  L2 = NULL;
  WSTAW ("bakłażan", &L2);
  WSTAW ("bambus", &L2);
  WSTAW ("borsuk", &L2);
  WSTAW ("bakłażan", &L2);
  WSTAW ("bóbr", &L2);
  printf ("Odbudowanie listy L2 w toku...\n");
  printf ("L1: "); DRUKUJ (L1);
  printf ("L2: "); DRUKUJ (L2);
  printf ("\n\n");

  printf("Lista L3 będąca scaleniem L1 i L2 to:\n");
  SCAL (&L1, &L2); DRUKUJ (L3);
  printf ("\n");
}
