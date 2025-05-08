use pubs;
select * from Products;

--stored procedure with OUT parameter
create proc proc_FilterProducts(@pcpu varchar(20), @pcount int out)
as 
begin
	set @pcount = (select count(*) from products where
	try_cast(json_value(details, '$.spec.cpu') as nvarchar(20)) = @pcpu);
end

begin
	declare @cnt int;
	exec proc_FilterProducts 'i7', @cnt out;
	print concat('The no. of computers is ', @cnt);
end


--Bulk Insert from a CSV file
create table people(
id int primary key,
name nvarchar(20),
age int);


create proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
	declare @insertQuery nvarchar(max)

	set @insertQuery = 'BULK INSERT people from '''+@filepath+'''
	with(
	FIRSTROW = 2,
	FIELDTERMINATOR = '','',
	ROWTERMINATOR = ''\n'')'

	exec sp_executesql @insertQuery;
end

exec proc_BulkInsert 'C:\Presidio-Genspark Training\Day 4 (08-05-2025)\Data.csv';

select * from people;


--procedure with structured exception handling
create table BulkInsertLog(
LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate());


create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
  begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'

	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  end catch
end

exec proc_BulkInsert 'C:\Presidio-Genspark Training\Day 4 (08-05-2025)\Dat.csv';
exec proc_BulkInsert 'C:\Presidio-Genspark Training\Day 4 (08-05-2025)\Data.csv';

select * from BulkInsertLog;

truncate table people;