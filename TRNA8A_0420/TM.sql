create or replace trigger tmtrigger before update on vasarlo for each row
begin
    insert into Naplo5 values('m�dos�t�s', :old.vid || '-' || :new.vid || '-' || :old.nev || '-' || :new.nev || '-' || :old.cim || '-' || :new.cim, current_timestamp);
end;