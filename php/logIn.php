<?php
	$connection = mysqli_connect('localhost', 'root', 'root', 'tronprototype',3307);
	if ($connection === false)
	{
		echo "Connection failed";
		exit();
	}

	$fileContents = file_get_contents("./logInTemp.txt");
	$explodedContents = explode("\t", $fileContents);
	$email = $explodedContents[0];
	$pWord = $explodedContents[1];

	$getDetailsQuery = "SELECT username, playerID FROM players WHERE email = '$email' AND pWord = '$pWord'";
	$getDetails = mysqli_query($connection, $getDetailsQuery) or die("Error retrieving username");
	$info = mysqli_fetch_assoc($getDetails);
	echo $info["username"] ."\t". $info["playerID"];
?>