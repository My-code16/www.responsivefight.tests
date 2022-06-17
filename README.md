#Test details
The test class **www.responsivefight.tests** comprises the following three tests method

**Website availability check**
Test to ensure the website is up by asserting the page title

**Registration**
Verifies registration with new user name and password. The code is handled for successful registration and for any errors in registration like mismatch in password or entering already used user name etc. 

**Leader board check**
This test log in the application with already registered user and tries to achieve the desired score by checking the leader board portal after every tests and runs until it reaches the expected score. 

**Run paramaters**

webBrowser - InternetExplorer = 0,Firefox = 1, Chrome = 2, Safari = 3, Edge = 4 (these are the codes for the particular browsers).
user_name  - "Enter your desired name" .
app_pwd	- "Enter your password"
existing_user_name - " Enter existing user name"
existing_pwd - " Enter existing user name"
expected_score -" Enter desired score to achieve" ( based on the entered score system will continue until it reaches it)

Note: Currently I have used given values for the above parameters. In order to try with new ones , need to change the run settings , rebuild and execute the code. 

There are three run settings one for each browsers ( Chrome , Edge, Firefox) available

How to run the code using Command prompt

Clone the repository www.responsivefight.tests to the local system 
 
Use the following commands to run in console 
- Enter the local file path in the below command and press enter

dotnet test "enter the path of local location"\www.responsivefight.tests\bin\Debug\netcoreapp3.1\www.responsivefight.tests.dll --logger:trx --settings "enter the path of local location"\www.responsivefight.tests\Edge.runsettings

dotnet test "enter the path of local location"\www.responsivefight.tests\bin\Debug\netcoreapp3.1\www.responsivefight.tests.dll --logger:trx --settings "enter the path of local location"\www.responsivefight.tests\Chrome.runsettings

dotnet test "enter the path of local location"\www.responsivefight.tests\bin\Debug\netcoreapp3.1\www.responsivefight.tests.dll --logger:trx --settings "enter the path of local location"\www.responsivefight.tests\Firefox.runsettings

How to run the code using Visual studio
  
Open the solution www.responsivefight.tests.sln from the local copied location 
Select desired settings using Test>Configure run settings >Select solution wide runsettings file > Chrome.runsettings  (Edit the run paramaters to the desired value) 

select Test > Run All test 
 output will be displayed in the Test explorer. To open test explorer goto Test > Test explorer 
 
 
Github CI/CD implementaion
For every PR raised for this repo and merged to master branch git workflow is defined and the output can be referred in the actions section of github 
