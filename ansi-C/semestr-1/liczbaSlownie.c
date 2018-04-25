#include <stdio.h>
#include <math.h>

int main() {
  char *jednosci[]={"zero", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć"};
  char *dziesiatki[]={"", "", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt", "dziesięć", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście"};
  char *setki[]={"", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset"};
  char *tysiace[]={"", "tysiąc", "dwa tysiące", "trzy tysiące", "cztery tysiące", "pięć tysięcy", "sześć tysięcy", "siedem tysięcy", "osiem tysięcy", "dziewięć tysięcy"}; 

  int liczbaN;

  printf("Podaj n (warunek: 0<=n<10000): \n");
  scanf("%d", &liczbaN);

  while (liczbaN<0 || liczbaN>=10000) {
    printf("Wprowadziłeś niepoprawne n. Wprowadź poprawne n: \n");
    scanf("%d", &liczbaN);
  }

  int iloscCyfr = 0, ostatniIndeks;

  if (liczbaN == 0)
    iloscCyfr=1;
  else
    iloscCyfr=(int)(log10(liczbaN)+1);

  ostatniIndeks = iloscCyfr - 1;
  
  char *wynik[iloscCyfr];

  char lista[iloscCyfr];
  sprintf(lista, "%d", liczbaN);
    
  int i, cyfraJednosci;
  
  for (i=ostatniIndeks;i>=0; i--) {  				
    int cyfra = lista[i] - '0';
    if (i==ostatniIndeks) {
      cyfraJednosci = cyfra;
      wynik[ostatniIndeks] = jednosci[cyfraJednosci];
    }
    else if (i==(ostatniIndeks - 1)) {				
      if (cyfraJednosci == 0) {
        wynik[ostatniIndeks] = "";
      }
      if (cyfra == 1) {
        wynik[ostatniIndeks] = "";
        wynik[ostatniIndeks -1] = dziesiatki[cyfraJednosci + 10];
        } else {
        wynik[ostatniIndeks - 1] = dziesiatki[cyfra];
        }
      }
      else if (i==ostatniIndeks -2) {
        wynik[ostatniIndeks - 2] = setki[cyfra];
      }
      else if (i==ostatniIndeks-3) {
        wynik[ostatniIndeks-3] = tysiace[cyfra];
      }
  }
    
  printf("Liczba n zapisana słownie to: \n");
  for (i=0; i<=ostatniIndeks; i++) {
    printf("%s ", wynik[i]);
  }
  printf("\n");   
  return 0;
}
