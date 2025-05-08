
--a function must mandatorily have a return statement; unlike procedure. (SCALAR VALUE FUNCTION)
create function fn_CalcTax(@baseprice float, @tax float)
returns float
as
begin
	return (@baseprice + (@baseprice*@tax/100))
end

select dbo.fn_CalcTax(1000,10) as price;


--a function that returns a table (TABLE VALUE FUNCTION)
create function fn_tableSample(@minprice FLOAT)
returns table
as
	return (select title, price from titles where price>@minprice)

select * from dbo.fn_tableSample(10) order by price;