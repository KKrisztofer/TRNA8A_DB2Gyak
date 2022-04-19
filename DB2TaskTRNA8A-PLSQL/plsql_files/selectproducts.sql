declare
    c_name products.name%type;
    c_description products.description%type;
    c_price products.price%type;
    cursor c_products is select name, description, price from products;
begin
    open c_products;
    loop
        fetch c_products into c_name, c_description, c_price;
        exit when c_products%notfound;
        dbms_output.put_line(c_name || ' ' || c_description || ' ' || c_price);
    end loop;
    close c_products;
end;