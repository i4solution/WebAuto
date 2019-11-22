<?php
	session_start();
	
	header("Access-Control-Allow-Origin: *");
	header("Access-Control-Allow-Methods: *");
	header("Content-Type: application/json; charset=UTF-8");
	
	$params   = explode('/', trim($_SERVER['PATH_INFO'],'/'));
	$endpoint = array_shift($params);

	// Lấy method của request
	$method         = $_SERVER['REQUEST_METHOD'];
	$allow_method   = array('GET', 'POST', 'PUT', 'DELETE');

	if ($endpoint == "read")
	{
		read();
	}
	else if ($endpoint == "create")
	{
		create();
	}else if ($endpoint == "checkversion")
	{
		CheckVersion();
	}
	else{
		//// set response code - 404 Not found
		http_response_code(404);
	 
		//// tell the user no products found
		echo json_encode(
		   array("message" => "Information incorrect."));
	}
	function create()
	{
		$usr = ($_POST['usr']);
		$pass = ($_POST['pass']);
		$email = ($_POST['email']);
		$phone = ($_POST['phone']);
		$servername = "localhost";
		$username = "id11416592_root";
		$password = "123456789";
		$dbname = "id11416592_bqdatabase";

		// Create connection
		$conn = new mysqli($servername, $username, $password, $dbname);
		// Check connection
		if ($conn->connect_error) 
		{
			die("Connection failed: " . $conn->connect_error);
			echo "Connection failed: " . $conn->connect_error;
		}
		$sql = "INSERT INTO bq_user (bq_UserId, bq_Password, bq_Email, bq_Phone) VALUES ('".$usr."', '".$pass."', '".$email."', '".$phone."')";

		if ($conn->query($sql) === TRUE) 
		{
			http_response_code(200);
			echo json_encode(
					array("message" => "Information OK."));
		}
		else
		{
			http_response_code(406);
		}
		$conn->close();
	}
	function read()
	{
		$usr = ($_POST['usr']);
		$pass = ($_POST['pass']);
		$servername = "localhost";
		$username = "id11416592_root";
		$password = "123456789";
		$dbname = "id11416592_bqdatabase";

		// Create connection
		$conn = new mysqli($servername, $username, $password, $dbname);
		// Check connection
		if ($conn->connect_error) 
		{
			die("Connection failed: " . $conn->connect_error);
			echo "Connection failed: " . $conn->connect_error;
		}
		$sql = "select * from bq_user where bq_UserId='".$usr."'";
		$result = $conn->query($sql);
	
		if ($result->num_rows > 0) 
		{
			$row = mysqli_fetch_array($result);
			//if (crypt("linh", '$1$usedatal$ZrwW2LD9Ur.h1wdBfu18N/') === '$1$usedatal$ZrwW2LD9Ur.h1wdBfu18N/')
			if ($row['bq_Password'] == $pass)
			{
				$_SESSION['userID'] = $usr;
				http_response_code(200);
				echo json_encode(
					array("message" => "Login OK."));
			}
			else
				http_response_code(406);
		} else 
		{
			http_response_code(406);
		}
		$conn->close();
	}
	function CheckVersion()
	{
		$myfile = fopen("bq_update_ver.txt", "r") or die("Unable to open file!");
		// Output one line until end-of-file
		$data="";
		while(!feof($myfile)) {
			$data = fgets($myfile);
		}
		fclose($myfile);
		http_response_code(200);
		echo json_encode(
			array("message" => $data));
	}
	
?> 