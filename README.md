Ett hobbyprojekt av Tobias Andersson

Detta är en liten applikation som skapar ett slumpmässigt lösenord, krypterar det och sedan sparar det i en lokal databas.
Iden till detta projektet fick jag efter att min sambo klagade på mig att jag alltid glömde bort mina lösenord till olika hemsidor. 
Istället för att förlita mina lösenord till en 3de part så skapade jag en applikation som skapar, krypterar, sparar mina lösenord med hjälp av en pinkod. 
Pinkoden använder jag sedan när jag vill hämta mina lösenord för att avkryptera dom. På det sättet så är det bara jag som kan avkryptera mina lösenord 
och på det sättet gör det inget om någon kommer in i databasen och ser lösenorden. De kommer ändå inte kunna avkryptera dom utan min personliga pinkod och 
använda dom för att logga in.

För att kunna köra applikationen så behöver man skapa en txt-fil med följande MySQLscript 

SERVER=localhost;DATABASE=passord;UID=root;PASSWORD="Fyll i ditt egna lösenord här";

Samt sätta upp en databas för lösenorden. Här är scriptet jag använde:

CREATE DATABASE passord;
USE passord;
CREATE TABLE lösenord(
Hemsida VARCHAR(100),
Lösenord VARCHAR(40));

När detta är gjort så är det bara att starta applikationen och köra. 

Eventuell feedback och förbättrings förslag kan man skicka till mig på tobiiasa@hotmail.com
Tack så mycket för din tid.
