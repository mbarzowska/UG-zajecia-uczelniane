sources = zad3.c zad4.c

programs = $(basename $(sources))
objects = $(addsuffix .o, $(programs))

zad1: zad1.c 
	gcc -E zad1.c

zad2: zad2.c
	gcc -E zad2.c

CFLAGS = -Wall -std=c11
LDLIBS = -lm

.PHONY : clean
clean :
	-$(RM) $(objects) $(programs) *~

all : $(programs)
