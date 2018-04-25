sources = zad1.c

programs = $(basename $(sources))
objects = $(addsuffix .o, $(programs))

CFLAGS = -Wall -std=c11
LDLIBS = -lm

.PHONY : clean
clean :
	-$(RM) $(objects) $(programs) *~

all : $(programs)
