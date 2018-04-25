<!doctype html>
<html>
  <meta charset="utf-8">
  <body>
<!--
// Zrealizować w PHP na sigmie licznik odwiedzin strony, działający również po zamknięciu sesji i wylogowaniu się.
// To znaczy, kiedy Państwo wrócą do domu, wyłączą swoje prywatne komputery i pójdą spać,
// to wizyty gości na postawionej przez Państwo stronie licznik ma nadal odnotowywać.
-->

<?php
  header("Pragma: no-cache");
?>

<?php
  $file= @fopen("Count.dat", "c+");
  fscanf($file, "%d", $count);

  $count++;
  rewind($file);
  fputs($file, $count);
  fclose($file);

  echo $count;
?>
  </body>
</html>
