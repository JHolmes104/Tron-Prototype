<?php

	$connection = mysqli_connect('localhost', 'root', 'root', 'tronprototype',3307);
	if ($connection === false)
	{
		echo "connection failed";
		exit();
	}
	$username = $_POST['username'];
	$email = $_POST['email'];
	$clanID = $_POST['clanID'];
	$pWord = $_POST['pWord'];
	$createUserQuery = "INSERT INTO players (username, email, clanID, pWord) VALUES ('$username', '$email', 1, '$pWord')";
	mysqli_query($connection, $createUserQuery) or die("Error creating user");
?>