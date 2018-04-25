// Napisać program w C wczytujący liczbę naturalną n≤3 i rysujący przy pomocy OpenGL n-kąt foremny.
// Uwaga: n-kąt nazywa się foremny, jeśli ma wszystkie boki równej długości i wszystkie kąty równe.

/* Program nie kompiluje się przez sugerowany na wykładzie makefile. Uruchamiany poprzez Code::Blocks działa prawidłowo. */

#include<stdio.h>
#include<GL/glut.h>
#include<math.h>

//==================== Definiowanie kolorów
#define CZARN 0.0, 0.0, 0.0
#define CZERW 1.0, 0.0, 0.0
#define ZIELO 0.0, 1.0, 0.0
#define NIEBI 0.0, 0.0, 1.0
#define BIALY 1.0, 1.0, 1.0

//==================== Geometria obrazka
void geometria(int w, int h) {
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  glOrtho(-20, 20, -15, 15, -4, 4);
  // glOrtho(lewa, prawa, dolna, gorna, przednia, tylna);
  glMatrixMode(GL_MODELVIEW);
}

//==================== Scena:
void wyswietl(int katy) {
  // czyszczenie buforu:
  glClear(GL_COLOR_BUFFER_BIT);
  // kolor tła:
  glClearColor(BIALY, 1.0);
  // definicja wielokąta:
  glBegin(GL_POLYGON);
  // n-kąt
  int i, n = 3; // liczba kątów, narazie określona

  double PI = 3.14, x, y;

  for (i=0; i < n; i++) {
    x = cos((2 * PI / n) * i);
    y = sin((2 * PI / n) * i);
    glVertex3d(x, y, 0);
  }
  
  // koniec definiowania:
  glEnd();
  // wykonanie
  glFlush();
}

//==================== Main:
int main ( int argc, char * argv[] )
{
  // inicjalizacja biblioteki GLUT
  glutInit( & argc, argv );

  // początkowy rozmiar okna:
  glutInitWindowSize(400, 400);

  // ustala początkową pozycję okna na ekranie:
  glutInitWindowPosition(200, 200);

  // tworzy okno:
  glutCreateWindow("Laboratorium_11_Zadanie_1");

  glutReshapeFunc(geometria);

  //wywołuje funkcję użytkownika rysującą scenę:
  glutDisplayFunc(wyswietl);

  glutMainLoop();
}
