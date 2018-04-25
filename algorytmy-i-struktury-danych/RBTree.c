#include<stdio.h>
#include<stdlib.h>                                     

typedef struct wezelDrzewa {                          
  int dane;
  char kolor;
  struct wezelDrzewa *ojciec;
  struct wezelDrzewa *lewy;
  struct wezelDrzewa *prawy;
}wezelDrzewa;

wezelDrzewa* utworzNowyWezel(int dane);
void wstawBST(wezelDrzewa **wezel, wezelDrzewa *nowyWezel);
void wstawRB(wezelDrzewa **wezel, int dane);         
void obrocLewo(wezelDrzewa** n, wezelDrzewa *x);        
void obrocPrawo(wezelDrzewa** n, wezelDrzewa *x);
void drukujInOrder(wezelDrzewa* wezel);
wezelDrzewa* znajdz(wezelDrzewa* wezel, int dane);    
void szukanieWypis(wezelDrzewa* temp);

wezelDrzewa* wezeldoWstawienia = NULL;                  

int main() {                                           
  wstawRB(&wezeldoWstawienia, 5);                     
  wstawRB(&wezeldoWstawienia, 3);
  wstawRB(&wezeldoWstawienia, 20);
  wstawRB(&wezeldoWstawienia, 1);
  wstawRB(&wezeldoWstawienia, 7);
  wstawRB(&wezeldoWstawienia, 4);
  wstawRB(&wezeldoWstawienia, 15);
  wstawRB(&wezeldoWstawienia, 6);
  wstawRB(&wezeldoWstawienia, 2);
  drukujInOrder(wezeldoWstawienia); printf("\n");

  wezelDrzewa* temp;                                   

  printf("Następuje wyszukiwanie elementu 2\n");
  temp = znajdz(wezeldoWstawienia, 2);                  
  szukanieWypis(temp);                                 

  printf("Następuje wyszukiwanie elementu 10\n");
  temp = znajdz(wezeldoWstawienia, 10);
  szukanieWypis(temp);
}

wezelDrzewa* utworzNowyWezel(int dane) {
  wezelDrzewa* utworzNowyWezel = (wezelDrzewa*)malloc(sizeof(wezelDrzewa));
  utworzNowyWezel->dane = dane;
  utworzNowyWezel->kolor = 'R';
  utworzNowyWezel->ojciec = NULL;
  utworzNowyWezel->lewy = NULL;
  utworzNowyWezel->prawy = NULL;
  return utworzNowyWezel;
} 

void wstawBST(wezelDrzewa **wezel, wezelDrzewa *nowyWezel) {
  if((*wezel)==NULL) {
    (*wezel) = nowyWezel;
  }
  if((nowyWezel->dane) < (*wezel)->dane) {
    wstawBST(&(*wezel)->lewy,nowyWezel);           
    (*wezel)->lewy->ojciec = *wezel;                
  }
  else if((nowyWezel->dane) > (*wezel)->dane) {
    wstawBST(&(*wezel)->prawy,nowyWezel);
    (*wezel)->prawy->ojciec = *wezel;
  }
} 

void wstawRB(wezelDrzewa **wezel, int dane) {
  wezelDrzewa* nowyWezel = utworzNowyWezel(dane);
  wstawBST(&wezeldoWstawienia, nowyWezel);  
  wezelDrzewa* dziadekRB = NULL;
  wezelDrzewa* ojciecRB = NULL;

  while ((nowyWezel != (*wezel)) && (nowyWezel->ojciec->kolor == 'R')) {
    dziadekRB = nowyWezel->ojciec->ojciec;
    ojciecRB = nowyWezel->ojciec;

    if(ojciecRB == dziadekRB->prawy) {            
      wezelDrzewa* wujekRB = dziadekRB->lewy;
      if(wujekRB != NULL && wujekRB->kolor == 'R') {
        ojciecRB->kolor = 'B';
        wujekRB->kolor = 'B';
        dziadekRB->kolor = 'R';
        nowyWezel = dziadekRB;
      }
      else {
        if(nowyWezel == ojciecRB->lewy) {
          obrocPrawo(wezel, ojciecRB);
          nowyWezel = ojciecRB;
          ojciecRB = nowyWezel->ojciec;
        }
        ojciecRB->kolor = 'B';
        dziadekRB->kolor = 'R';
        obrocLewo(wezel, dziadekRB);
      }
    }
    else {
      wezelDrzewa* wujekRB = dziadekRB->prawy;
      if(wujekRB != NULL && wujekRB->kolor == 'R') {
        ojciecRB->kolor = 'B';
        wujekRB->kolor = 'B';
        dziadekRB->kolor = 'R';
        nowyWezel = dziadekRB;
      }
      else {
        if(nowyWezel == ojciecRB->prawy) {
          obrocLewo(wezel, ojciecRB);
          nowyWezel = ojciecRB;
          ojciecRB = nowyWezel->ojciec;
        }
        ojciecRB->kolor = 'B';
        dziadekRB->kolor = 'R';
        obrocPrawo(wezel, dziadekRB);
      }
    }
  }
(*wezel)->kolor = 'B';                 
} 

void drukujInOrder(wezelDrzewa* wezel) {
  if(wezel==NULL) {
    return;
  }
//  printf("(");
  drukujInOrder(wezel->lewy);    
//  printf(",");
  if(wezel->ojciec == NULL) {                       
    printf("To korzeń ->");
  }
  printf("Węzeł %d:%c\n", wezel->dane, wezel->kolor);
//  printf(",");
  drukujInOrder(wezel->prawy);
//  printf(")");
}

//-----
void obrocLewo(wezelDrzewa **wezel, wezelDrzewa *x) {
/* Cormen T. : Strukturę wskaźnikową zmienia się za pomocą rotacji, która jest lokalną operacją na drzewie poszukiwań binarnych, zachowującą uporządkowanie
inorder kluczy w drzewie. Na rysunku 14.2 są pokazane dwa rodzaje rotacji: lewe i prawe. Lewą rotację na węźle x można wykonać tylko wtedy, gdy jego prawy syn y nie jest równy NIL. Lewa rotacja polega na "obrocie" wokół krawędzimiędzy węzłami x i y. W wyniku rotacji y staje się nowym korzeniem poddrzewa, x zostaje jego lewym synem, a lewy syn węzła y zostaje prawym synem węzła x.
*/
  wezelDrzewa *y = x->prawy;
  x->prawy = y->lewy;
  if(y->lewy != NULL) {
    y->lewy->ojciec = x;
  }
  y->ojciec = x->ojciec;
  if(x->ojciec == NULL) {
    (*wezel) = y;
  }
  else if(x == x->ojciec->lewy) {
    x->ojciec->lewy = y;
  }
  else {
    x->ojciec->prawy = y;
  }
  y->lewy = x;
  x->ojciec = y;
}

//-----
void obrocPrawo(wezelDrzewa **wezel, wezelDrzewa *x) {
  wezelDrzewa* y = x->lewy;
  x->lewy = y->prawy;
  if(y->prawy != NULL) {
    y->prawy->ojciec = x;
  }
  y->ojciec = x->ojciec;
  if(x->ojciec == NULL) {
    (*wezel) = y;
  }
  else if(x == x->ojciec->lewy) {
    x->ojciec->lewy = y;
  }
  else {
    x->ojciec->prawy = y;
  }
  y->prawy = x;
  x->ojciec = y;
}

//-----
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

//-----
void szukanieWypis(wezelDrzewa* temp) {
  if(temp==NULL)                                    
    printf("Element nie został znaleziony\n\n");
  else
    printf("Element został znaleziony, jego kolor to: %c\n\n", temp->kolor);
}
