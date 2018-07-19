declare @i int=0;
declare @length int =20;

while @i <@length
begin
insert into [dbo].[客戶聯絡人]
([客戶Id],[職稱],[姓名],[Email],[手機],[電話],[IsDeleted])
--記得客戶ID要調成新建的
values(1002,N'跑腿','test'+(cast(@i as nvarchar(2))),'test'+(cast(@i as nvarchar(2))+'@abc.com'),'0987654321','2987654321',0);
set @i=@i+1;
end;