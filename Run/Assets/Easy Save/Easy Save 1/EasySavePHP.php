<?
// Note: All errors should begin with "ERROR" as this lets Unity tell the
// difference between error messages and save data.


// *** MYSQL DB DETAILS ***
$host = "mysite.com.mysql"; //MySQL Host name
$user = "mysite_com"; //MySQL User Name
$dbpassword = "mysqlpassword"; //MySQL Password

$dbname = "myDatabase"; //MySQL Database Name
$tableName = "easysavedata"; //Name of the table used to store data
$fieldName = "savedata"; //The name of the field where you want to save the data
$idFieldName = "saveid"; //The name of the field where you want to save the ID

$unityPassword = "UnityPassword"; //This should match the phpPassword set in Unity

// Function to sanitize against SQL injection
function sanitize($sql, $formUse = true)
{
	$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
	$sql = trim($sql);
	$sql = strip_tags($sql);
	if(!$formUse || !get_magic_quotes_gpc())
	  $sql = addslashes($sql);
	return $sql;
}

//Get password specified in Unity (which has been converted to MD5 in Unity).
$md5Password = sanitize($_POST["password"]);

//If Unity password doesn't match our password, reject request.
if($md5Password != md5($unityPassword))
{
	echo "ERROR: Couldnt access database with these credentials";
}
//Else if we specified to upload in Unity, run upload code.
else if(sanitize($_POST["upload"])=="true")
{
	$savefile = sanitize($_POST["save"]); //Get save data as string
	
	if(!$savefile) //If there's no save file, reject request.
	{
	    echo "ERROR: Save file is empty.";
	}
	else
	{
		$saveID = sanitize($_POST["saveID"]); //Get save ID
		if(!$saveID)
		{
			$saveID = ""; //If no save ID specified, set to default.
		}
		//Run our SQL query, and generate an error if it fails.
		mysql_connect($host, $user, $dbpassword) or die("ERROR: Cant connect into database.");
		mysql_select_db($dbname)or die("ERROR: Cant select database.");
        $SQL = "INSERT INTO $tableName ($fieldName,$idFieldName) VALUES ('$savefile','$saveID') ON DUPLICATE KEY UPDATE $fieldName = '$savefile'";
        $result = @mysql_query($SQL) or die("ERROR: SQL Query Failed. " . mysql_error());
        
		mysql_close(); // Close MySQL connection
	}
}
//Else, we must want to download a save file, so run download code.
else
{
	$saveID = sanitize($_POST["saveID"]);
	if(!$saveID)
	{
		$saveID = ""; //If no save ID specified, set to default.
	}
	
	//Run our SQL query, and generate an error if it fails.
	mysql_connect($host, $user, $dbpassword) or die("ERROR: Cant connect into database.");
	mysql_select_db($dbname)or die("ERROR: Cant select database.");
	$SQL = "SELECT $fieldName FROM $tableName WHERE $idFieldName='$saveID'";
	$result = @mysql_query($SQL) or die("ERROR: SQL Query Failed. " . mysql_error());
	
	if(mysql_num_rows($result) == 1) //If file is found, echo it so Unity can read it.
	{
		echo mysql_result($result, 0, "savedata");
	}
	else
	{
		echo "ERROR: A file with this ID does not exist";
	}

	mysql_close(); // Close MySQL connection
}
?>