

		create or replace procedure updateproductname(p_id number, p_newname char) is
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

