<!DOCTYPE HTML>

<html lang="pl">
<meta charset="utf-8">

<head>
  <title>Laboratorium 9, Zadanie 1 - Kalkulator</title>
</head>

<body>
  <?php
    $arg1 = $_POST["arg1"];
    $arg2 = $_POST["arg2"];
    $operator = $_POST["operator"];

    if(isset($arg1) AND isset($operator) AND isset($arg2)) {
      if ($operator == "+") {
        $wyliczenie = $arg1 + $arg2;
        echo "Suma : $wyliczenie";
      }

      if ($operator == "-") {
        $wyliczenie = $arg1 - $arg2;
        echo "Różnica : $wyliczenie";
       }
    
      if ($operator == "*") {
        $wyliczenie = $arg1 * $arg2;
        echo "Mnożenie : $wyliczenie";
      }
    
      if ($operator == "/") {
        $wyliczenie = $arg1 / $arg2;
        echo "Dzielenie : $wyliczenie";
      }
    }
    else
      echo "Jakis argument nie został wpisany lub nie zaznaczono opcji obliczenia"
  ?>

  </body>
</html>
