// Napisać funkcję typu void, która ma
//    dwa całkowite parametry wejściowe n i k, k≠0,
//    dwa całkowite parametry wyjściowe:
//        q równy ilorazowi całkowitemu n przez k, oraz
//        r równy reszcie z dzielenia n przez k.

// Ta funkcja nie może mieć żadnych innych parametrów, nie może odwoływać się do zmiennych globalnych i oczywiście nie może niczego czytać ani pisać -- jej jedyna komunikacja z resztą programu odbywa się przez parametry.

// Napisać program, który wczytuje dwie liczby całkowite i wywołuje powyższą funkcję, a następnie drukuje iloraz całkowity oraz resztę z dzielenia wczytanych liczb.

#include<stdio.h>

void funkcja(int n, int k, int *q, int *r)
{
	*q = n / k;
	*r = n % k;
}

int main()
{
	int liczba1, liczba2, iloraz, reszta;

	printf("Liczba 1:\n");
	scanf("%d", &liczba1);

	printf("Liczba 2 (≠0):\n");
	scanf("%d", &liczba2);

	funkcja(liczba1, liczba2, &iloraz, &reszta);

	printf("Otrzymane wyniki:\n");
	printf("Iloraz całkowity: %d, Reszta z dzielenia: %d\n", iloraz, reszta);
}
