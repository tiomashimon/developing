select * from order
where
(LastName like 'A%' 
or LastName like '1%')
and OrderAmount between 100 and 200;