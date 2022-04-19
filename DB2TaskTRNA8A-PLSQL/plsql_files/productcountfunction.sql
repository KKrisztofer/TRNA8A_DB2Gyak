create or replace function productcount return int as f_result int;
begin
select count(id) into f_result from products;
return(f_result);
end;