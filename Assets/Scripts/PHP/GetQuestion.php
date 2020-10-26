<?php
     header("Content-Type: application/json; charset=UTF-8");
     $hostname = 'mysql.metropolia.fi';
     $username = 'joonaaal';
     $password = 't13t0k1lp4';
     $database = 'joonaaal';
 
     $con = mysqli_connect($hostname, $username, $password, $database);
 
     if(mysqli_connect_errno()){
 
        echo "Failed to connect " . mysqli_connect_errno();
     }
 
     $result = mysqli_query($con, "Select kysymys,vaihtoehto1,vaihtoehto2,vaihtoehto3,vaihtoehto4 FROM kysymykset Where id = 74");
 
 
     while ($row = mysqli_fetch_array($result)) {
        echo $row['kysymys']."_". $row['vaihtoehto1']."_". $row['vaihtoehto2'] ."_". $row['vaihtoehto3'] ."_". $row['vaihtoehto4']  . PHP_EOL;
 
     }
     mysqli_close($con);
 ?>
