var fullprice = 0;
function addProduct(id, name, price) {
    document.getElementById("empty").classList.add("hide");
    if(document.getElementById("products").value == "")
    {
        document.getElementById("products").value = id;
    }
    else
    {
        document.getElementById("products").value = document.getElementById("products").value + "," + id;
    }
    document.getElementById("cart").innerHTML += "<br>" + name;
    fullprice = fullprice + price;
    document.getElementById("price").value = fullprice + " Ft";
    document.getElementById("fullprice").value = fullprice;
}