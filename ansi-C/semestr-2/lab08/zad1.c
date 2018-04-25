// Jakie będą skutki rozwinięcia następującej makrodefinicji?
// Najpierw zgadnąć, a potem sprawdzić komendą
// gcc -E plik

#define INFO(kraj, waluta) W kraj OBOWIAZUJE waluta.
INFO(Polsce, zloty)
INFO(Rosji, rubel)
INFO(Slowacji, euro)

// Program wypisze: 
// W Polsce OBOWIAZUJE zloty.
// W Rosji OBOWIAZUJE rubel.
// W Slowacji OBOWIAZUJE euro.

