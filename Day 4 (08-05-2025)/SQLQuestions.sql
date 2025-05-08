select * from customers;
select * from employees;
select * from orders;
select * from categories;
select * from suppliers;
select * from products;

--1)list all orders with customer name and employee who handled the order
select o.orderid Order_ID, c.contactname Customer_name, concat(e.firstname,' ',e.lastname) Employee_Name
from orders o join customers c
on o.customerID = c.customerID join employees e
on o.employeeid = e.employeeid order by order_id;

--2)list of products along with their category and supplier name
select p.productid Product_ID, p.productname Product_Name, c.categoryname Category, 
s.companyname Supplier_Company, s.contactname Supplier_Contact
from products p join categories c
on p.CategoryID = c.categoryid join suppliers s
on p.SupplierID = s.supplierid order by Product_ID;

--3)show all orders and products included in each order with qty and unit price
select * from "order details";
select o.orderid, p.productname, od.quantity, od.unitprice
from orders o join "order details" od
on o.orderid = od.orderid join products p
on p.productid = od.productid
order by unitprice;

--4)list employees who report to other employees
select concat(e1.firstname,' ',e1.lastname) Employee, concat(e2.firstname,' ',e2.lastname) Reports_To
from employees e1 join employees e2 on e2.employeeid = e1.reportsto
order by employee;

--5)display each customer and their total order count
select c.customerid Customer_ID, count(o.orderid) Total_Order_Count from customers c join orders o
on c.customerid = o.customerid
group by c.customerid order by customer_id;

--6)find avg unit price of products per category
select categoryid Product_Category, avg(unitprice) Avg_Price from products
group by categoryid;

--7)list customers where the contact title starts with 'owner'
select contactname, contacttitle from customers 
where contacttitle like 'Owner%' order by contactname;

--8)show the top 5 most expensive products
select top 5 productid, productname, unitprice from products 
order by unitprice desc;

--9)return the total sales amount(qty x unitprice) per order
select od.orderid Order_ID, sum(od.unitprice*od.quantity) Total_Amount from "order details" od
group by od.orderid;

--10)create a stored procedure that returns all orders for a given customer id
create or alter proc proc_GetAllOrders(@cid nvarchar(5))
as
begin
	select orderid Orders from orders
	where customerid = @cid;
end

exec proc_GetAllOrders 'ANTON';


--11)stored procedure that inserts a new product
create or alter proc proc_InsertData(@pname nvarchar(50), @supid int, @catid int, @unitprice float, @stock int, @orderunits int,
@reorderlvl int, @disc int)
as
begin
	insert into products(ProductName,SupplierID,CategoryID,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued)
	values(@pname, @supid, @catid, @unitprice, @stock, @orderunits, @reorderlvl, @disc);
end

exec proc_InsertData 'Tea', 15, 1, 20.00, 15, 7, 10, 0;


--12)stored procedure that returns total sales per employee
select * from orders;
select * from "order details";
select * from employees;

create or alter proc proc_GetSalesPerEmp
as
begin
	select e.employeeid Employee_ID, count(o.orderid) Total_Sales from employees e join orders o
	on e.employeeid = o.employeeid
	group by e.employeeid;
end

exec proc_GetSalesPerEmp


--13)use a CTE to rank products by unit price within each category (high to low)
select * from Products;
with ProductRank as
(select productid, productname, categoryid, unitprice,
ROW_NUMBER() over (partition by categoryid order by unitprice desc) as ProdRank from products)

select * from ProductRank;


--14)create a CTE to calculate total revenue per product and filter products with revenue > 10,000
with ProdRevenue as
(select productid Prod_ID, sum(unitprice*quantity) Total_Revenue
from "order details" group by productid)

select * from ProdRevenue where Total_Revenue > 10000;


--15)use a CTE with recursion to display employee hierarchy
with EmplHierarchy as
(select employeeid, concat(firstname,' ',lastname) Empl_Name, reportsto from employees
where reportsto is null
union all
select e.employeeid, concat(e.firstname,' ',e.lastname) Empl_Name, e.reportsto 
from employees e join EmplHierarchy eh on e.reportsto = eh.employeeid)

select * from EmplHierarchy;