#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#define STRING 100          

int linie_pliku_do_rozmiaru(char *nazwa);
void swap(int tablica[], int a, int b);
void tworz_tablice(char *nazwa, int tablica[], int rozmiar);
void drukuj_tablice(int tablica[], int rozmiar);
void zapisz_tablice(char *nazwa, int tablica[], int rozmiar);

int left(int i);
int right(int i);

void BuildHeap(int tablica[], int rozmiar);
void HeapSort(int tablica[],int rozmiar);
void Heapify(int tablica[], int ojciec, int rozmiar);

int main(int argc, char* argv[]) {            
	if(argc == 3) {                                      
		char nazwa[STRING];
  	char nazwa1[STRING];                                  
		sscanf(argv[1], "%s", nazwa);
		sscanf(argv[2], "%s", nazwa1);

		int rozmiar;
		rozmiar = linie_pliku_do_rozmiaru(nazwa)-1;

		int tablica[rozmiar];
		tworz_tablice(nazwa, tablica, rozmiar);
		printf("\nWczytano tablicę z pliku %s :\n", nazwa);
		drukuj_tablice(tablica, rozmiar);

		HeapSort(tablica, rozmiar);
		printf("\nPosortowana tablica:\n");
		drukuj_tablice(tablica, rozmiar);

		printf("Zapisano tablicę do pliku %s.\n\n", nazwa1);
		zapisz_tablice(nazwa1, tablica, rozmiar);
		return 1;
	}
	else {
		printf("Niepoprawna komenda wywołania.\nPrawidłowy format to\n./a.out nazwa-pliku-wejścia(tutaj: input.txt) nazwa-pliku-wyjścia(dowolna)\n");
    exit(0);
	}
}

int linie_pliku_do_rozmiaru(char *nazwa) {
	FILE *file = fopen(nazwa, "r");
	if (file == NULL) 	{
		printf("\nNie mozna otworzyć pliku %s!\nSprawdź, czy istnieje\n", nazwa);
		exit(0);
	}

	int counter = 0;
	int ch;

	do {
      ch = fgetc(file);                                       
  		if(ch == '\n')                                        
  			counter++;                                        
	} while (ch != EOF);

	fclose(file);
	return counter;
}

void swap (int tablica[], int a, int b) {               
	int pom;
	pom = tablica[a];
	tablica[a] = tablica[b];
	tablica[b] = pom;
}

void tworz_tablice(char *nazwa, int tablica[], int rozmiar) {
	FILE *file = fopen(nazwa, "r");
	if (file == NULL) {
		printf("\nNie mozna otworzyć pliku %s!\nSprawdź, czy istnieje\n", nazwa);
		exit(0);
	}

	int i;
	for(i = 0; i < rozmiar; i++) {
		fscanf(file, "%d\n", &tablica[i]);
	}
	fclose(file);
}

void drukuj_tablice(int tablica[], int rozmiar) {
  int i;
	for(i = 0; i < rozmiar; i++) {
		printf("%d ", tablica[i]);
	}
	printf("\n");
}

void zapisz_tablice(char *nazwa, int tablica[], int rozmiar) {
  int i;
  FILE *file = fopen(nazwa, "w");                 

	for(i = 0; i < rozmiar; i++) {
		fprintf(file, "%d\n", tablica[i]);
	}
	fclose(file);
}

int left (int i) {
  if(i == 0)
    return 1;
  else
    return 2*i;
};

int right (int i) {
  if(i == 0)
    return 2;
  else
    return 2*i+1;
};

void BuildHeap(int tablica[], int rozmiar) {     
  int i;
  for (i = rozmiar/2; i >= 0; i--)
    Heapify(tablica, i, rozmiar);
}

void HeapSort(int tablica[], int rozmiar) {
  BuildHeap(tablica, rozmiar);
  int r = rozmiar;

  for (int i = r-1; i >= 1; --i) {                  
    swap(tablica, i, 0);
    --r;
    Heapify(tablica, 0, r);
  }
}

void Heapify(int tablica[], int ojciec, int rozmiar) {
  while (ojciec <= rozmiar) {
    int largest;
    int lewy = left(ojciec);                         
    int prawy = right(ojciec);

  if (lewy < rozmiar && tablica[lewy] > tablica[ojciec]) 
    largest = lewy;                                     
  else                                                 
		largest = ojciec;                              

  if (prawy < rozmiar && tablica[prawy] > tablica[largest])
		largest = prawy;                                   

	if (largest != ojciec) {                               
		swap(tablica, ojciec, largest);                    
		ojciec = largest;                                  
  }
  else
		break;
  }
}
