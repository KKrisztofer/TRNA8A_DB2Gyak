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
		<title>Termékek</title>
		<meta charset="utf-8">
        <link rel="stylesheet" href="style.css">
        <script type="text/javascript" src="script.js"></script>
	</head>
	<body>
        <div class="frame">

            <?php
                $sql = "SELECT * FROM products";
                $result = $conn->query($sql);
                
                if ($result->num_rows > 0) {
                  while($row = $result->fetch_assoc()) {
                    echo "<div class=\"product\">";
                        echo "<div class=\"name\">";
                            echo "<p>" . $row["name"] . "</p>";
                        echo "</div>";
                        echo "<div class=\"description\">";
                            echo "<p>" . $row["description"] . "</p>";
                        echo "</div>";
                        echo "<div class=\"price\">";
                            echo "<p>" . $row["price"] . " Ft</p>";
                        echo "</div>";
                        echo "<div class=\"tocart\">";
                            echo "<input type=\"button\" value=\"kosárba\" onclick=\"addProduct(" . $row["id"] . ", '" . $row["name"] .  "', " . $row["price"] . ")\">";
                        echo "</div>";
                    echo "</div>";
                  }
                } else {
                  echo "0 results";
                }
                $conn->close();
            ?>

        </div>
        <div class="frame">
            <div class="formframe">
                <div>
                    <p class="productsselected">Kiválasztott termékek:</p>
                    <p id="empty">Nincs kiválasztva termék!</p>
                    <p id="cart"></p>
                </div>
                <form action="order.php" method="post">
                    <input type="text" name="price" id="price" placeholder="ár (automatikusan számított mező)" readonly>
                    <input type="text" name="fullprice" id="fullprice" class="hide">
                    <input type="text" name="lastname" placeholder="vezetéknév">
                    <input type="text" name="firstname" placeholder="keresztnév">
                    <input type="text" name="phone" placeholder="telefonszám">
                    <input type="text" name="mail" placeholder="email">
                    <input type="text" name="postcode" placeholder="irányítószám">
                    <input type="text" name="city" placeholder="város">
                    <input type="text" name="address" placeholder="cím">
                    <input type="text" name="products" id="products" class="hide">
                    <input type="submit" value="rendelés leadása">
                </form>
            </div>
        </div>

	</body>
</html>