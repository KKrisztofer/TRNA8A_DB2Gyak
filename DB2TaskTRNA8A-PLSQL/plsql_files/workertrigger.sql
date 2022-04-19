create or replace trigger workertrigger
before insert or delete or update on workers for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'besz�r�s', 'workers');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 't�rl�s', 'workers');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'm�dos�t�s', 'workers');
    end if;
end;