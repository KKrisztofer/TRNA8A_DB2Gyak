create or replace procedure updateproduct(p_id number, p_newname char, p_newdescription char, p_newprice char) is
notexsisterror exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found = 0 then raise notexsisterror;
    else
        update products set name = p_newname, description = p_newdescription, price = p_newprice where id = p_id;
    end if;
exception
    when notexsisterror then
    dbms_output.put_line('Ilyen azonosítójú termék nem létezik!');
end;