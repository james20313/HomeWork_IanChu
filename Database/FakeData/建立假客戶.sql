declare @i int=0;
declare @length int =20;

while @i <@length
begin
insert into [dbo].[客戶資料]
([客戶名稱],[統一編號],[電話],[傳真],[地址],[Email],[IsDeleted],[類別Id])
values('test'+cast(@i as nvarchar(2)),'51015548','23456789','29876543','test','test@abc.com',0,1);
set @i=@i+1;
end;