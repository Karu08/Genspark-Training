use pubs;
select * from authors;

with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) as author_name from authors)

select * from cteAuthors;


--fetching first 10 records using row_number()
declare @page int =1, @pageSize int=10;
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*(@pageSize+1)) and (@page*@pageSize);


--create a procedure that will take the page number and size as param and print the books
create or alter proc proc_PaginateTitles( @page int =1, @pageSize int=10)
as
begin
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*(@pageSize+1)) and (@page*@pageSize)
end

exec proc_PaginateTitles 2,5;


--using offset instead of row_number()
select  title_id,title, price
from titles
order by price desc
offset 10 rows fetch next 10 rows only;


