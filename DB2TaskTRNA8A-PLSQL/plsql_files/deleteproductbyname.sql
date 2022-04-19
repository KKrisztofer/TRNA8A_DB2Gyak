create or replace procedure deleteproductbyname(p_name char) is
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