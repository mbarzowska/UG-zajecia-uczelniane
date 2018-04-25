#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>


double get_unix_time (void) {                       
  struct timespec tv;
  if (clock_gettime(CLOCK_REALTIME, &tv) != 0) return 0;
  return (tv.tv_sec + (tv.tv_nsec / 1000000000.0));
}

void swap (int tablica[], int a, int b) {
	int pom;
	pom = tablica[a];
	tablica[a] = tablica[b];
	tablica[b] = pom;
}

int partition (int A[], int p, int r) {
  int x = A[r]; int j;
  int i = p-1;
  for (j = p; j <= r; j++) {
    if (A[j] <= x) {
      i++;
      swap (A, i, j);
    }
  }
  if (i < r) return i;
  else return i - 1;
}

void quicksort (int A[], int p, int r) {
  int q;
  if (p < r) {
    q = partition (A, p, r);
    quicksort (A, p, q);
    quicksort (A, q+1, r);
  }
}

void generuj_losowa_tablice (int *A, int rozmiar) {           
  srand (time(NULL));                                         
  for (int i = 0; i < rozmiar ; i++)
    A[i] = rand()%999;
}

void generuj_niekorzystna_tablice (int *A, int rozmiar) { 
    int j = rozmiar;
    for (int i = 0; i < rozmiar; i++, j--)
      A[i] = j;
}

void sprawdz_czas_random (double *czas) {
  int t2500[2500], t1000[1000], t500[500], t100[100];
  double Tn_start, Tn_stop;

  generuj_losowa_tablice (t100, 100);
  Tn_start = get_unix_time ();    quicksort (t100, 0, 100);
  Tn_stop  = get_unix_time ();    czas[0] = Tn_stop - Tn_start;

  generuj_losowa_tablice (t500, 500);
  Tn_start = get_unix_time ();    quicksort (t500, 0, 500);
  Tn_stop  = get_unix_time ();    czas[1] = Tn_stop - Tn_start;

  generuj_losowa_tablice (t1000, 1000);
  Tn_start = get_unix_time ();    quicksort (t1000, 0, 1000);
  Tn_stop  = get_unix_time ();    czas[2] = Tn_stop - Tn_start;

  generuj_losowa_tablice (t2500, 2500);
  Tn_start = get_unix_time ();    quicksort (t2500, 0, 2500);
  Tn_stop  = get_unix_time ();    czas[3] = Tn_stop - Tn_start;
}

void sprawdz_czas_niekorzystna (double *czas) {
  int t2500[2500], t1000[1000], t500[500], t100[100];
  double Tn_start, Tn_stop;

  generuj_niekorzystna_tablice (t100, 100);
  Tn_start = get_unix_time ();    quicksort (t100, 0, 100);
  Tn_stop  = get_unix_time ();    czas[0] = Tn_stop - Tn_start;

  generuj_niekorzystna_tablice (t500, 500);
  Tn_start = get_unix_time ();    quicksort (t500, 0, 500);
  Tn_stop  = get_unix_time ();    czas[1] = Tn_stop - Tn_start;

  generuj_niekorzystna_tablice (t1000, 1000);
  Tn_start = get_unix_time ();    quicksort (t1000, 0, 1000);
  Tn_stop  = get_unix_time ();    czas[2] = Tn_stop - Tn_start;

  generuj_niekorzystna_tablice (t2500, 2500);
  Tn_start = get_unix_time ();    quicksort (t2500, 0, 2500);
  Tn_stop  = get_unix_time ();    czas[3] = Tn_stop - Tn_start;
}

  int main() {
    double czas[4];

    sprawdz_czas_random(czas);
    printf ("Dane losowe:\n");
    printf( "rozmiar tablicy N  | czas działania algorytmu |\n");
    printf( "      100          |   %.10lf           | \n",czas[0]);
    printf( "      500          |   %.10lf           | \n",czas[1]);
    printf( "      1000         |   %.10lf           | \n",czas[2]);
    printf( "      2500         |   %.10lf           | \n",czas[3]);


    sprawdz_czas_niekorzystna(czas);
    printf ("Dane niekorzystne:\n");
    printf( "rozmiar tablicy N  | czas działania algorytmu |\n");
    printf( "      100          |   %.10lf           | \n",czas[0]);
    printf( "      500          |   %.10lf           | \n",czas[1]);
    printf( "      1000         |   %.10lf           | \n",czas[2]);
    printf( "      2500         |   %.10lf           | \n",czas[3]);
  }
