create or replace procedure deleteproductbyid(p_id number) is
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