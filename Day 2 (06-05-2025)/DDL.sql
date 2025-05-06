create database shop;
use shop;

create table Products(prod_id int primary key not null, prod_name varchar(20), inventory_qty int, unit_price float);
create table Suppliers(sup_id int primary key not null, sup_name varchar(20), addr_id int);
create table Prod_Sup(prod_id int, sup_id int, 
foreign key(prod_id) references Products(prod_id), foreign key(sup_id) references Suppliers(sup_id));
create table Sup_Address(addr_id int primary key not null, flat_name varchar(20), area varchar(20), pincode int, city_id int);
create table City(city_id int primary key not null, city_name varchar(20), state_id int);
create table State(state_id int primary key not null, state_name varchar(20));
create table Customers(cust_id int primary key not null, name varchar(20), email varchar(30), phone varchar(15));