sources = zad1.c zad2.c zad3.c zad4.c zadanie_dodatkowe_podkreslenie.c

programs = $(basename $(sources))
objects = $(addsuffix .o, $(programs))

CFLAGS = -Wall -std=c11
LDLIBS = -lm

.PHONY : clean
clean :
	-$(RM) $(objects) $(programs) *~

all : $(programs)
