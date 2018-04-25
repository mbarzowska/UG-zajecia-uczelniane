<?php
session_start();
$_SESSION['indeks']=$_REQUEST['ile'];
$_SESSION['indeks']=$_SESSION['indeks']+1;
$_SESSION['nick'.$_SESSION['indeks']] = $_REQUEST['nowy'];
header('Location: l9z2_zapisy.php');
?>
