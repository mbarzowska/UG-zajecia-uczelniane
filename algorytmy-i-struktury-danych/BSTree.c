#include<stdio.h>
#include<stdlib.h>

typedef struct wezelDrzewa {
  int dane;
  struct wezelDrzewa *lewy;
  struct wezelDrzewa *prawy;
}wezelDrzewa;

wezelDrzewa * wstaw(wezelDrzewa *wezel, int dane);
wezelDrzewa * znajdz(wezelDrzewa *wezel, int dane);
void szukanieWypis(wezelDrzewa* temp);
wezelDrzewa * usun(wezelDrzewa *wezel, int dane);
void drukujInOrder(wezelDrzewa *wezel);
wezelDrzewa* najwiekszyElement(wezelDrzewa *wezel);
wezelDrzewa* najmniejszyElement(wezelDrzewa *wezel);


int main() {
  wezelDrzewa *root = NULL;
  root = wstaw(root, 11);
  root = wstaw(root, 2);
  root = wstaw(root, 33);
  root = wstaw(root, 4);
  root = wstaw(root, 55);
  root = wstaw(root, 6);
  root = wstaw(root, 77);
  root = wstaw(root, 8);
  drukujInOrder(root); printf("\n\n");

  printf("Następuje próba usunięcia elementu 11\n");
  root = usun(root, 11);
  drukujInOrder(root); printf("\n\n");

  printf("Następuje próba usunięcia elementu 99\n");
  root = usun(root, 99);
  drukujInOrder(root); printf("\n\n");

  wezelDrzewa * temp;

  printf("Następuje wyszukiwanie elementu 2\n");
  temp = znajdz(root, 2);
  szukanieWypis(temp);

  printf("Następuje wyszukiwanie elementu 10\n");
  temp = znajdz(root, 10);
  szukanieWypis(temp);
}

wezelDrzewa* najmniejszyElement(wezelDrzewa *wezel) {
  if(wezel==NULL)
    return NULL;
  else if(wezel->lewy)
    return najmniejszyElement(wezel->lewy);
  else
    return wezel;
}

wezelDrzewa* najwiekszyElement(wezelDrzewa *wezel) {
  if(wezel==NULL)
    return NULL;
  else if(wezel->prawy)
    najwiekszyElement(wezel->prawy);
  else
    return wezel;
}

wezelDrzewa* wstaw(wezelDrzewa *wezel,int dane) {
  if(wezel==NULL) {
    wezelDrzewa *temp;
    temp = (wezelDrzewa *)malloc(sizeof(wezelDrzewa));
    temp -> dane = dane;
    temp -> lewy = temp -> prawy = NULL;
    return temp;
  }
  if(dane > (wezel->dane))
    wezel->prawy = wstaw(wezel->prawy,dane);
  else if(dane < (wezel->dane))
    wezel->lewy = wstaw(wezel->lewy,dane);
  else
    return wezel;
}

wezelDrzewa* usun(wezelDrzewa *wezel, int dane) {
  wezelDrzewa *temp;
  if(wezel==NULL)
    printf("Elementu nie było w tablicy!\n");
  else if(dane < wezel->dane)
    wezel->lewy = usun(wezel->lewy, dane);
  else if(dane > wezel->dane)
    wezel->prawy = usun(wezel->prawy, dane);
  else {
    if(wezel->prawy && wezel->lewy) {
      temp = najmniejszyElement(wezel->prawy);
      wezel->dane = temp->dane;
      wezel->prawy = usun(wezel->prawy,temp->dane);
    }
    else {
      temp = wezel;
      if(wezel->lewy == NULL)
        wezel = wezel->prawy;
      else if(wezel->prawy == NULL)
        wezel = wezel->lewy;
      free(temp);
    }
  }
  return wezel;
}

wezelDrzewa* znajdz(wezelDrzewa *wezel, int dane) {
  if(wezel==NULL) {
    return NULL;
  }
  if(dane > wezel->dane) { 
    return znajdz(wezel->prawy,dane);
  }
  else if(dane < wezel->dane) {
    return znajdz(wezel->lewy,dane);
  }
  else {                      
    return wezel;
  }
}

void szukanieWypis(wezelDrzewa* temp) {
  if(temp==NULL)
    printf("Element nie został znaleziony\n\n");
  else
    printf("Element został znaleziony\n\n");
}

void drukujInOrder(wezelDrzewa* wezel) {
  if(wezel==NULL) {
    return;
  }
  drukujInOrder(wezel->lewy);
  printf("%d ",wezel->dane);
  drukujInOrder(wezel->prawy);
}
