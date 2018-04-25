// Napisać program, który wczytuje datę (dzień, miesiąc, rok) i podaje wypadający wtedy dzień tygodnia. 

#include<stdio.h>

char *tydzien[]={"poniedziałek", "wtorek", "środa", "czwartek", "piątek", "sobota", "niedziela"};

int czyPrzestepny(int rok) {
  return ((rok % 4 == 0  &&  rok % 100 != 0) || rok % 400 == 0);
}  // 1 jezeli podany rok jest przestepny, 0 w przeciwnym wypadku

int wyznaczDzienTygodnia(int dzien, int miesiac, int rok) {
  int liczbaDni[] = {0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334};
  // ile dni minęło dla danego miesiąca od początku roku (w roku nieprzestępnym)

  int dzienRoku;
  int yy, c, g;
  int dzienTygodnia;

  dzienRoku = dzien + liczbaDni[miesiac-1];
  if ((miesiac > 2) && (czyPrzestepny(rok) == 1))
    dzienRoku++;

  yy = (rok - 1) % 100;
  c = (rok - 1) - yy;
  g = yy + (yy / 4);

  dzienTygodnia = (((((c / 100) % 4) * 5) + g) % 7);
  dzienTygodnia += dzienRoku - 1;
  dzienTygodnia %= 7;

  return dzienTygodnia;
} 

int main() {
  int m, r, d, dzienTygodnia;

  printf("Podaj datę, dla której należy obliczyć dzień tygodnia:\n");

  printf("Podaj dzień:\n");
  scanf("%d", &d);
  printf("Podaj miesiąc:\n");
  scanf("%d", &m);
  printf("Podaj rok:\n");
  scanf("%d", &r);

  dzienTygodnia = wyznaczDzienTygodnia(d, m, r);
  printf("Dzien tygodnia dla podanej daty to %s.\n", tydzien[dzienTygodnia]);
}
