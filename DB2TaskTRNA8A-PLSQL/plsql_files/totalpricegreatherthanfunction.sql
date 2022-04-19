create or replace function totalpricegreaterthan(f_price in int) return int as f_result int;
begin
    select sum(price) into f_result from products where price > f_price;
    return(f_result);
end;