#include<stdio.h>
#include<stdlib.h>
#include <time.h>

void swap (int *a, int *b);
int znajdzMediane(int tablica[], int ilosc_elementow);
int podzielTablice(int tablica[], int lewy, int prawy, int klucz);
int algorytmMagicznychPiatek(int tablica[], int lewy, int prawy, int klucz);

int main() {
    int tablica[] = {3,50,60,63,11,4,5,85,70,99,61,101,62,19,22,10,30,1,100,9,82,21,40,71,20,80,81,79};
    int klucz = 17;
    int ilosc_elementow;
    ilosc_elementow = sizeof(tablica)/sizeof(*tablica);

    int algorytm;
    algorytm = algorytmMagicznychPiatek(tablica, 0, ilosc_elementow-1, klucz);

    printf("K-ty najmniejszy element to %d\n", algorytm);
    return 0;
}

int znajdzMediane(int tablica[], int ilosc_elementow) {
    int i, p, temp, pozycja;
    for (i = 0; i < (ilosc_elementow-1); i++) {
        pozycja = i;
        for (p = i + 1; p < ilosc_elementow; p++) {
            if (tablica[pozycja] > tablica[p])
            pozycja = p;
        }
        if (pozycja != i) {
          swap(&tablica[i], &tablica[pozycja]);
        }
    }
    return tablica[ilosc_elementow/2];   
}

int algorytmMagicznychPiatek(int tablica[], int lewy, int prawy, int klucz) {
    if (klucz > 0 && klucz <= prawy - lewy + 1) {
        int ilosc_elementow = prawy - lewy + 1; 

        int i, tablicaMedian[(ilosc_elementow+4)/5];
        for (i = 0; i < ilosc_elementow/5; i++)
            tablicaMedian[i] = znajdzMediane(tablica + lewy + i*5, 5);
        if (i*5 < ilosc_elementow) {   
            tablicaMedian[i] = znajdzMediane(tablica+lewy+i*5, ilosc_elementow%5);
            i++;
        }

        int medianaMedian = (i == 1)? tablicaMedian[i-1]:
                                 algorytmMagicznychPiatek(tablicaMedian, 0, i-1, i/2);

        int pozycjaElWyznPodzial = podzielTablice(tablica, lewy, prawy, medianaMedian);

        if (pozycjaElWyznPodzial-lewy == klucz-1)
          return tablica[pozycjaElWyznPodzial];
        else if (pozycjaElWyznPodzial-lewy > klucz-1)
          return algorytmMagicznychPiatek(tablica, lewy, pozycjaElWyznPodzial-1, klucz);
        else
          return algorytmMagicznychPiatek(tablica, pozycjaElWyznPodzial+1, prawy, klucz-pozycjaElWyznPodzial+lewy-1);
    }
}

void swap (int *a, int *b) {
    int temp = *a;
    *a = *b;
    *b = temp;
}

int podzielTablice(int tablica[], int lewy, int prawy, int punktPodzialu) {
    int i, j;
    for (i = lewy; i < prawy; i++)
        if (tablica[i] == punktPodzialu)
           break;
    swap(&tablica[i], &tablica[prawy]);

    i = lewy;
    for (j = lewy; j <= prawy - 1; j++) {
        if (tablica[j] <= punktPodzialu) {
            swap(&tablica[i], &tablica[j]);
            i++;
        }
    }
    swap(&tablica[i], &tablica[prawy]);
    return i;
}
