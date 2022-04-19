create or replace trigger customertrigger
before insert or delete or update on customers for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'besz�r�s', 'customers');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 't�rl�s', 'customers');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'm�dos�t�s', 'customers');
    end if;
end;