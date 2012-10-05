<?
// Note: All errors should begin with "ERROR" as this lets Unity tell the
// difference between error messages and save data.

// Feel free to modify this file to suit your needs.


// *** MYSQL DB DETAILS ***
$host = "yourwebsite.com.mysql"; 	// MySQL Host Name.
$user = "yourwebsite_com"; 			// MySQL User Name.
$dbpassword = "yourDBPassword"; 	// MySQL Password.

$dbname = "yourDatabaseName"; 	// MySQL Database Name.
$tableName = "easysavedata"; 	// Name of the table used to store data.
$dataFieldName = "savedata"; 	// The name of the field where you want to save the data.
$idFieldName = "saveid"; 		// The name of the field where you want to save the ID.
$tagFieldName = "tag"; 			// The name of the field where you want to save the tag.
// * * * * * * * * * * * * *

// *** GENERAL DETAILS ***
$unityUsername = "EasySave";		//This should match the webUsername set in Unity.
$unityPassword = "65w84e4p994z3Oq"; //This should match the webPassword set in Unity.
$defaultSaveID = 0; 				//If no saveID is specified, it will use this as the saveID.
// * * * * * * * * * * * 

//If Unity username or password doesn't match our username or password, reject request.
if($unityUsername != sanitize($_POST["username"]) || sanitize($_POST["password"]) != md5($unityPassword))
{
	echo "ERROR: Couldnt access database with these credentials.";
}
//Else if we specified to upload in Unity, run upload code.
else if(sanitize($_POST["mode"]=="overwrite") || sanitize($_POST["mode"]=="append"))
{
	$savefile = sanitize($_POST["data"]); //Get save data as string
	
	if(!$savefile) //If there's no save file, reject request.
	{
	    echo "ERROR: Save file is empty.";
	}
	else
	{
		$saveID = sanitize($_GET["id"]); //Get save ID
		if(!$saveID)
		{
			$saveID = $defaultSaveID; //If no save ID specified, set to default.
		}
		$tag = sanitize($_GET["tag"]);
		if(!$tag)
		{
			$tag = ""; //If no tag specified, use blank string.
		}
		
		//Run our SQL query, and generate an error if it fails.
		mysql_connect($host, $user, $dbpassword) or die("ERROR: Cant connect into database.");
		mysql_select_db($dbname) or die("ERROR: Cant select database.");
		
		if($_POST["mode"]=="overwrite")
		{
        	$SQL = "INSERT INTO $tableName ($dataFieldName,$idFieldName,$tagFieldName) VALUES ('$savefile','$saveID','$tag') ON DUPLICATE KEY UPDATE $dataFieldName = '$savefile'";
		}
        else
        {
        	$SQL = "INSERT INTO $tableName ($dataFieldName,$idFieldName,$tagFieldName) VALUES ('$savefile','$saveID','$tag') ON DUPLICATE KEY UPDATE $dataFieldName = CONCAT($dataFieldName, '$savefile')";
        }
        $result = @mysql_query($SQL) or die("ERROR: SQL Query Failed. " . mysql_error());
        
		mysql_close(); // Close MySQL connection
	}
}
//Else, we must want to download a save file, so run download code.
else if(sanitize($_POST["mode"]=="load"))
{
	$saveID = sanitize($_GET["id"]);
	$tag = sanitize($_GET["tag"]);
	if(!$saveID)
	{
		$saveID = $defaultSaveID; //If no save ID specified, set to default.
	}
	if(!$tag)
	{
		$tag = ""; //If no tag specified, use blank string.
	}
	
	//Run our SQL query, and generate an error if it fails.
	mysql_connect($host, $user, $dbpassword) or die("ERROR: Cant connect into database.");
	mysql_select_db($dbname) or die("ERROR: Cant select database.");
	$SQL = "SELECT $dataFieldName FROM $tableName WHERE $idFieldName='$saveID' AND $tagFieldName='$tag'";
	$result = @mysql_query($SQL) or die("ERROR: SQL Query Failed. " . mysql_error());
	
	if(mysql_num_rows($result) == 1) //If file is found, echo it so Unity can read it.
	{
		echo mysql_result($result, 0, $dataFieldName);
	}
	
	mysql_close(); // Close MySQL connection
}
else
{
	echo "ERROR: Unknown mode has been specified. Data should only be loaded using Easy Save from within Unity.";
}

// Function to sanitize against SQL injection
function sanitize($sql)
{
	$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
	return $sql;
}

// Generate

?>