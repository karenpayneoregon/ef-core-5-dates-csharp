SELECT Id, 
       FirstName, 
       LastName, BirthDate,
       FORMAT(BirthDate, 'MM/dd/yyyy') AS BirthDate1
FROM DateTimeDatabase.dbo.Birthdays 
WHERE BirthDate BETWEEN '01/02/1953' AND '09/24/1956'
ORDER BY BirthDate;

SELECT EventID, 
       FORMAT(StartDate, 'MM/dd/yyyy'), 
       FORMAT(EndDate, 'MM/dd/yyyy')
FROM DateTimeDatabase.dbo.Events
WHERE dbo.Events.StartDate BETWEEN '01/02/2017' AND '01/04/2017';
