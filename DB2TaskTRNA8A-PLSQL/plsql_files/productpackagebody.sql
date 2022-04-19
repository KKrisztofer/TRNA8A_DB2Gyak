create or replace package body productPackage is
procedure newProduct(p_name char, p_description char, p_price char) is
begin
    insert into products values(null, p_name, p_description, p_price, current_timestamp);
end;

procedure updateProductName(p_id number, p_newname char) is
notexsisterror exception;
nameequalerror exception;
rows_found number;
current_name char(20);
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found !=0 then
        select name
        into current_name
        from products
        where id = p_id;
    end if;
    
    if rows_found = 0 then raise notexsisterror;
    elsif p_newname = current_name then raise nameequalerror;
    else
        update products set name = p_newname where id = p_id;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen azonosítójú termék nem létezik!');
    when nameequalerror then
    dbms_output.put_line('Ugyanaz a név van megadva!');
end;

procedure updateProductDescription(p_id number, p_newdescription char) is
notexsisterror exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found = 0 then raise notexsisterror;
    else
        update products set description = p_newdescription where id = p_id;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen azonosítójú termék nem létezik!');
end;

procedure updateProductPrice(p_id number, p_newprice number) is
notexsisterror exception;
priceisnotvalid exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found = 0 then raise notexsisterror;
    elsif p_newprice < 1 then raise priceisnotvalid;
    else
        update products set price = p_newprice where id = p_id;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen azonosítójú termék nem létezik!');
    when priceisnotvalid then
    dbms_output.put_line('A megadott ár nem megfelelõ!');
end;

procedure deleteProductById(p_id number) is
notexsisterror exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found = 0 then raise notexsisterror;
    else
        delete from products where id = p_id;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen azonosítójú termék nem létezik!');
end;

procedure deleteProductByName(p_name char) is
notexsisterror exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where name = p_name;
    
    if rows_found = 0 then raise notexsisterror;
    else
        delete from products where name = p_name;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen termék nem létezik!');
end;

function totalPriceGreaterThan(f_price in int) return int is f_result int;
begin
    select sum(price) into f_result from products where price > f_price;
    return(f_result);
end;

end productPackage;