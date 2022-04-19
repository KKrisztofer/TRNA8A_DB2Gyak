

		create or replace procedure updateproductdescription(p_id number, p_newdescription char) is
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

