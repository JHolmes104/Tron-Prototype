<?php
	$connection = mysqli_connect('localhost', 'root', 'root', 'tronprototype',3307);
	if ($connection === false)
	{
		echo "Connection failed";
		exit();
	}

	$clanName = $_POST['clanName'];
	$playerID = $_POST['playerID'];

	$createClanQuery = "INSERT INTO clans (clanName, clanLeaderID) VALUES ('$clanName', '$playerID')";
	mysqli_query($connection, $createClanQuery) or die ("Error creating clan");
	
	$getClanIDQuery = "SELECT clanID FROM clans WHERE clanName = '$clanName'";
	$getClanID = mysqli_query($connection, $getClanIDQuery) or die("Error retrieving clanID");
	$info = mysqli_fetch_assoc($getClanID);
	$clanID = $info['clanID'];

	$changeClanIDQuery = "UPDATE players SET clanID = '$clanID' WHERE playerID = '$playerID'";
	mysqli_query($connection, $changeClanIDQuery) or die("Error changing clanID");
?>