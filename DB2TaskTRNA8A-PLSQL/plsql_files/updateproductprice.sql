create or replace procedure updateproductprice(p_id number, p_newprice number) is
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