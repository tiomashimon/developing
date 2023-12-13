select * from order
where 
LastName like 'A%'
and OrderAmount is not null
and LastName not in ('Shevcenko')
and OrderId between 10 and 20;

