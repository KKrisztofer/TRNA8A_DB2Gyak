<!doctype html>
<?php
    $servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "db2task";

    $conn = new mysqli($servername, $username, $password, $dbname);

    if ($conn->connect_error){
        die("A kapcsolódás meghíúsult: " . $conn->connect_error);
    }
?>
<html lang="hu">
	<head>
		<title>Rendelés</title>
		<meta charset="utf-8">
        <link rel="stylesheet" href="style.css">
	</head>
	<body>
        <div class="frame">
            <?php
                $sql = "INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES('" . $_POST["firstname"] . "','" . $_POST["lastname"] . "','" . $_POST["mail"] . "','" . $_POST["address"] . "','" . $_POST["city"] . "'," . $_POST["postcode"] . ",'" . $_POST["phone"] . "',NOW())";

                $products = explode(",", $_POST["products"]);

                //for($i=0;$i<count($products);$i++){
                //    echo $products[$i];
                //}
                $success = false;
                if ($conn->query($sql) === TRUE) {
                    
                    $last_id = $conn->insert_id;

                    //$sql = "INSERT INTO orders(price, order_date, customer_id) VALUES (" . $_POST["fullprice"] . ",NOW()," . $last_id . ")";
                    $sql = "INSERT INTO orders(price, order_date, customer_id) VALUES (" . intval($_POST["fullprice"]) . ",NOW()," . $last_id . ")";
                    
                    if ($conn->query($sql) === TRUE) {
                        
                        $last_id = $conn->insert_id;

                        for($i=0;$i<count($products);$i++){
                            $sql = "INSERT INTO orderitems(order_id, product_id) VALUES (" . $last_id . "," . $products[$i] . ")";
                            if ($conn->query($sql) === TRUE) {
                                $success = true;
                            } else {
                                echo "A rendelés leadása sikertelen: " . $sql . "<br>" . $conn->error;
                            }
                        }

                    } else {
                        echo "A rendelés leadása sikertelen: " . $sql . "<br>" . $conn->error;
                    }
                } else {
                  echo "A rendelés leadása sikertelen: " . $sql . "<br>" . $conn->error;
                }
                if ($success)
                {
                    echo "A rendelés leadása sikeres!";
                } else {
                    echo "A rendelés leadása sikertelen!";
                }
                
                $conn->close();
            ?>
        </div>
	</body>
</html>