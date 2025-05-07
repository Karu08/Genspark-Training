use pubs;

--inner join
select title, pub_name from titles join publishers
on titles.pub_id = publishers.pub_id;


--retrieve publisher details of publisher who has never published
select * from publishers where pub_id not in
(select distinct pub_id from titles);


--right outer join
select title, pub_name from titles right outer join publishers
on titles.pub_id = publishers.pub_id;


--select author_if for all books; print author_id and book_name
select au_id, title from titleauthor left outer join titles
on titleauthor.title_id = titles.title_id;


--print publisher's name, book name, and order date of the books
select p.pub_name 'Publisher_name', t.title 'Book_title', s.ord_date 'Order_date'
from publishers p join titles t on p.pub_id=t.pub_id
join sales s on t.title_id=s.title_id;


--print the publisher name and date of first book sale for all the publishers
select pub_name 'Publisher_name', min(ord_date) 'First_order_date' from publishers left outer join titles
on publishers.pub_id=titles.pub_id left outer join sales on titles.title_id=sales.title_id
group by pub_name order by First_order_date desc;


--print book name and store address of the sale
select title 'Book_name', stor_address 'Store_address' from titles join sales
on titles.title_id=sales.title_id join stores 
on sales.stor_id=stores.stor_id order by Book_name;