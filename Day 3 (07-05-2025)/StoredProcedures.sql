
create procedure proc_FirstProcedure
as
begin
	print 'Hello World!'
end

exec proc_FirstProcedure;

create table Products
(id int identity(1,1) constraint pk_productId primary key,
name nvarchar(100) not null,
details nvarchar(max));

create procedure proc_InsertProduct(@pname nvarchar(100), @pdetails nvarchar(max))
as
begin
	insert into Products(name,details) values(@pname,@pdetails)
end

proc_InsertProduct 'Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"i7"}}'


select * from Products;

create or alter proc proc_InsertProduct(@pname nvarchar(100),@pdetails nvarchar(max))
as
begin
    insert into Products(name,details) values(@pname,@pdetails)
end

select JSON_QUERY(details, '$.spec') as Product_Specification from products;

select details from products;


create proc proc_UpdateProductSpec(@pid int, @newvalue varchar(20))
as
begin
   update products set details = JSON_MODIFY(details, '$.spec.ram' ,@newvalue) where id = @pid
end

exec proc_UpdateProductSpec 3, '24GB'

--after updating RAM
select id, name, JSON_VALUE(details, '$.brand') Brand_Name, JSON_Query(details, '$.spec') as Specifications
from Products

select * from products where 
  try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) ='i7'


--POSTS
create table posts(
id int primary key,
user_id int,
title nvarchar(100),
body nvarchar(max));

GO
select * from posts;


--procedure for bulk insertion
create proc proc_BulkInsertPosts(@jsondata nvarchar(max))
as
begin
	insert into posts(user_id, id, title, body)
	select userId, id, title, body from openjson(@jsondata)
	with (userId int, id int, title nvarchar(100), body nvarchar(max))
end

exec proc_BulkInsertPosts
'[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  },
 {
    "userId": 2,
    "id": 11,
    "title": "et ea vero quia laudantium autem",
    "body": "delectus reiciendis molestiae occaecati non minima eveniet qui voluptatibus\naccusamus in eum beatae sit\nvel qui neque voluptates ut commodi qui incidunt\nut animi commodi"
  }
]';

select * from posts;


--procedure that brings post by taking the user_id as parameter
create proc proc_GetUserId(@uid int)
as
begin
	select * from posts 
	where user_id = @uid;
end

proc_GetUserId 1;
 