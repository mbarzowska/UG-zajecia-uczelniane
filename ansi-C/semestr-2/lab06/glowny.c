/*=====================================================
 * Funkcja
 *   Boolean form0(drzewo* drz)
 * wczytuje formule logiczna i produkuje drzewo tej formuly.
 *
 * Formula na wejsciu musi byc zgodna z gramatyka:
 *
 *   form0  ::=  form1 { '=' form1 }*
 *   form1  ::=  form2  |  form2 '>' form1
 *   form2  ::=  form3 { 'V' form3 }*
 *   form3  ::=  form4 { '&' form4 }*
 *   form4  ::=  form5 | '~' form4
 *   form5  ::=  zm | st | '(' from0 ')'
 *
 * np.:  (p&q > r) = (p > (q > r)) .
 *
 * Zmienne logiczne: pojedyncze male litery.
 * Stale logiczne: znaki '0' i '1'.
 *
 * Operatory wg pierwszenstw wykonywania (od najsilniejszego do
 * najslabszego):
 *   ~  (negacja),
 *   &  (koniunkcja -- wiaze do lewej),
 *   V  (alternatywa -- wiaze do lewej),
 *   >  (implikacja -- wiaze do prawej),
 *   =  (rownowaznosc -- wiaze do lewej).
 * Oczywiscie mozna zmienic kolejnosc wykonywania przez zastosowanie
 * nawiasow.
 *
 * Ponizej nadane sa poczatkowe wartosci zmiennym.  Oczywiscie mozna
 * je poprawic zaleznie od wlasnych potrzeb.
 *
 *
 * Program nalezy kompilowac komenda
 *   gcc -Wall glowny.c funkcje.c -std=gnu99
 * i wywolywac komenda
 *   ./a.out
 *
 *=====================================================
*/
#include "funkcje.h"
int main(int argnum, char* arg[])
{
  drzewo drz;
  int tautologia = 1, spelnialna = 0;
  char tab[10] =
  { /* a= */ '0',  /* b= */ '0',  /* c= */ '0',  /* d= */ '0',
    /* e= */ '0',  /* f= */ '0',  /* g= */ '0',  /* h= */ '0',
    /* i= */ '0',  /* j= */ '0' };
  if(argnum==1) poziom=0;
  else poziom=2;
  nowy_lex();
  if(form0(&drz))
  {
    int i;
    for(i=0;i<1024 && (tautologia==1 || spelnialna==0);i++)
    {
      if(i%2==1) tab[0]='1'; else tab[0]='0';
      if((i>>1)%2==1) tab[1]='1'; else tab[1]='0';
      if((i>>2)%2==1) tab[2]='1'; else tab[2]='0';
      if((i>>3)%2==1) tab[3]='1'; else tab[3]='0';
      if((i>>4)%2==1) tab[4]='1'; else tab[4]='0';
      if((i>>5)%2==1) tab[5]='1'; else tab[5]='0';
      if((i>>6)%2==1) tab[6]='1'; else tab[6]='0';
      if((i>>7)%2==1) tab[7]='1'; else tab[7]='0';
      if((i>>8)%2==1) tab[8]='1'; else tab[8]='0';
      if((i>>9)%2==1) tab[9]='1'; else tab[9]='0';
      if(wylicz(drz, tab)=='0')
      {
        if(tautologia==1)
        {
          tautologia=0;
          printf("\nNie jest tautologia, bo dla wartosciowania: a:%c b:%c c:%c d:%c e:%c f:%c g:%c h:%c i:%c j:%c wartosc wyrazenia jest rowna 0\n",tab[0],tab[1],tab[2],tab[3],tab[4],tab[5],tab[6],tab[7],tab[8],tab[9]);
        }
      }
      else
      {
        if(spelnialna==0)
        {
          spelnialna=1;
          printf("\nJest spelnialna, bo dla wartosciowania: a:%c b:%c c:%c d:%c e:%c f:%c g:%c h:%c i:%c j:%c wartosc wyrazenia jest rowna 1\n",tab[0],tab[1],tab[2],tab[3],tab[4],tab[5],tab[6],tab[7],tab[8],tab[9]);
        }
      }
    }
  }
  else blad("nie formula");
  printf("\nTautologia: %s\n", tautologia ? "TAK" : "NIE");
  printf("Spelnialna: %s\n\n", spelnialna ? "TAK" : "NIE");
  if(lex!=EOF && lex!='\n') blad("smieci na koncu");
  return 0;
}
