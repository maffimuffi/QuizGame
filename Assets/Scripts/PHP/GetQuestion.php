<?php
     header('Content-type: text/html; charset=ISO-8859-1');
     $hostname = 'mysql.metropolia.fi';
     $username = 'joonaaal';
     $password = 't13t0k1lp4';
     $database = 'joonaaal';
     
     
     /*if(isset($_REQUEST["var1"]))
     {
         echo "received ". $_REQUEST["var1"]. " success!";
         
     }
     else
     {
         echo "Request Failed";
     }*/
     
 
     $con = mysqli_connect($hostname, $username, $password, $database);
 
     if(mysqli_connect_errno()){
 
        echo "Failed to connect " . mysqli_connect_errno();
     }
 
     $result = mysqli_query($con, $_REQUEST["var1"]);
     echo ";";
 
     while ($row = mysqli_fetch_array($result)) {
        echo $row['kysymys']."_". $row['vaihtoehto1']."_". $row['vaihtoehto2'] ."_". $row['vaihtoehto3'] ."_". $row['vaihtoehto4']. "_".
        $row['vaihtoehto5'] ."_" .$row['vaihtoehto6'] ."_".$row['vaihtoehto7'] ."_".$row['vaihtoehto8'] ."_".$row['vaihtoehto9'] ."_".$row['vaihtoehto10'] ."_"
        ."/"  . PHP_EOL;
         
     }
     echo $_REQUEST["var1"];
     mysqli_close($con);
 ?>
