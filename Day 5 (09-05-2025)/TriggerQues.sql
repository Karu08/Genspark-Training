--1)Write a trigger that logs whenever a new customer is inserted.
create table cust_log
(cust_id int,
inserted_time timestamp default now());

create or replace function log_insert()
returns trigger as
$$
begin
	insert into cust_log(cust_id) values(new.customer_id);
	return new;
end;
$$
language plpgsql;

create trigger trg_log_cust
after insert on customer
for each row execute function log_insert();

insert into customer values(
1002,1,'Harry','Potter'	,'jared.ely@sakilacustomer.org',530,true,
'2006-02-14','2013-05-26 14:49:45.738',1);

select * from customer where customer_id=1001;


--2)Create a trigger that prevents inserting a payment of amount 0.
create or replace function prevent_payment_zero()
returns trigger as
$$
begin
	if new.amount = 0 then
		raise exception 'Payment amount cannot be 0';
	end if;
	return new;
end;
$$
language plpgsql;

create trigger trg_prevent_payment_zero
before insert on payment
for each row execute function prevent_payment_zero();


--3)Set up a trigger to automatically set last_update on the film table before update.
create or replace function update_last()
returns trigger as
$$
begin
	new.last_update := now();
	return new;
end;
$$
language plpgsql;

create trigger trg_update_last
before update on film
for each row execute function update_last();


--4)Create a trigger to log changes in the inventory table (insert/delete).
create table inventory_log
(action text,
inventory_id int,
changed_at timestamp default now());

create or replace function log_changes()
returns trigger as
$$
begin
	if tg_op = 'insert' then
		insert into inventory_log(action, inventory_id) values('insert',new.inventory_id);
	elseif tg_op = 'delete' then
		insert into inventory_log(action, inventory_id) values('delete',old.inventory_id);
	end if;
	return null;
end;
$$
language plpgsql;

create trigger trg_log_changes
after insert or delete on inventory
for each row execute function log_changes();


--5)Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.
create or replace function block_high_rentals()
returns trigger as
$$
declare total_due numeric;
begin
	select sum(amount) into total_due from payment
	where customer_id = new.customer_id;
	if total_due>50 then
		raise exception 'customer owes more than $50, so cannot rent';
	end if;
	return new;
end;
$$
language plpgsql;

create trigger trg_block_high_rentals
before insert on rental
for each row execute function block_high_rentals();