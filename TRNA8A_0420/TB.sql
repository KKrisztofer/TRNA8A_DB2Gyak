create or replace trigger tbtrigger before insert on vasarlo for each row
begin
    insert into Naplo5 values('besz�r�s', :new.vid || :new.nev || :new.cim, current_timestamp);
end;