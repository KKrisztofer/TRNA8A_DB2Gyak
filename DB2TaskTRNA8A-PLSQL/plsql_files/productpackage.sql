create or replace package productPackage is
    procedure newProduct(p_name char, p_description char, p_price char);
    procedure updateProductName(p_id number, p_newname char);
    procedure updateProductDescription(p_id number, p_newdescription char);
    procedure updateProductPrice(p_id number, p_newprice number);
    procedure deleteProductById(p_id number);
    procedure deleteProductByName(p_name char);
    function totalPriceGreaterThan(f_price in int) return int;
end productPackage;