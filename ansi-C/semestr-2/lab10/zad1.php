<?php
function wyswietlKlikniecie() {
  $file = @fopen("licznik.txt", "c+");
  fscanf($file, "%d", $liczba);
  rewind($file);
  fclose($file);
  echo $liczba;
}

function klikPlus() {
  $file = @fopen("licznik.txt", "c+");
  fscanf($file, "%d", $liczba);
  $liczba = $liczba + 1;
  rewind($file);
  fputs($file, $liczba);
  fclose($file);
}
?>

<!DOCTYPE HTML>
<html lang="pl">

  <head>
    <meta charset="utf-8" />
    <title>Laboratorium 10, Zadanie 1 - Licznik kliknięć</title>
  </head>

  <body>
    <form action="" method="post">
      <input type="submit" value="Klikniesz? Kliknij." name="klik"><br>
    </form>

    <?php
      if (isset($_POST["klik"]) AND isset($_COOKIE["ciasteczko"])) {
        wyswietlKlikniecie();
        echo "\r\nNie oszukuj! Klikanie tylko raz na dzień!";
      }
      elseif (isset($_POST["klik"])) {
        setcookie("ciasteczko", $_POST["klik"], time() + 86400, "/");
        klikPlus();
        wyswietlKlikniecie();
      }
    ?>
  </body>

</html>
