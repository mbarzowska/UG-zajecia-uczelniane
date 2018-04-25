sources = stefan.c zad0.c zad1.c zad2.c zad3_szyfrowanie.c zad3_deszyfrowanie.c

programs = $(basename $(sources))
objects = $(addsuffix .o, $(programs))

CFLAGS = -Wall -std=c11
LDLIBS = -lm

.PHONY : clean
clean :
	-$(RM) $(objects) $(programs) *~

all : $(programs)
