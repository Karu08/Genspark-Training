rollback;
--1)Write a transaction that inserts a customer and an initial rental in one atomic operation.
begin;
insert into customer values(
1005,1,'John','Green','john.green@sakilacustomer.org',530,true,
'2006-02-14','2013-05-26 14:49:45.738',0);

insert into rental values(
20000,'2005-05-24 22:54:33',1525,408,'2005-05-28 19:40:33',1,'2006-02-16 02:30:53');
commit;


--2)Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.
begin;
update film
set rental_duration = rental_duration + 1 where film_id = 1;

insert into inventory(film_id, store_id) values(1,null);
rollback;


--3)Create a transaction that transfers an inventory item from one store to another.
begin;
update inventory
set store_id = 2
where inventory_id = 100 and store_id = 1;
commit;


--4)Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.
begin;
update payment
set amount = amount + 5 where payment_id = 101;

savepoint sp;

update payment
set amount = amount + 10 where payment_id = 102;

rollback to savepoint sp;
commit;


--5)Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
begin;
delete from payment where customer_id = 5;
delete from rental where customer_id = 5;
delete from customer where customer_id = 5;
commit;