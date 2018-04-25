<?php
session_start();
?>

<html land="pl">

  <head>
    <meta charset="utf-8" />
    <title>Robię imprezę z okazji 6000 dnia milenium. Zapisz się!/title>
  </head>

  <body>

    <div style="float:left; vertical-align:top; background-color:violet">
      <h1>Zapisy na imprezę:</h1>
        <?php
          if(isset($_SESSION['indeks']))
          for($i=1; $i<=$_SESSION['indeks']; $i++) {
            echo $i;
            echo '. ';
            echo $_SESSION['nick'.$i] ;
            echo '<br>';
          }
        ?>
    </div>

    <div style="float:right; vertical-align:top;background-color:pink">
      <h1>Dopisz się:</h1>
      <form action="l9z2_impreza.php">
       <input type="text" name="nowy">
       <input type="hidden"
                   name="ile"
                   value="<?php
                   if(isset($_SESSION['indeks'])) echo $_SESSION['indeks']; else echo '0';?>"       >
       <input type="submit" value="ZAPISZ">
      </form>
    </div>
  </body>
</html>
