Project Assessment 
GENERAL INSTRUCTIONS 
Please carefully read the below instructions. 
The objective of this assessment is to check your ability to complete a project as per the provided 
“Project Design”. 
You are expected to – 
1. Write the source code for the classes, methods and namespaces EXACTLY as mentioned 
in the “Project Design” section. 
2. Ensure that the names of the namespaces, classes, methods and variables EXACTLY 
MATCH with the names specified in the “Project Design” section. 
3. Understand the project requirements and ACCORDINGLY WRITE the code and logic 
in the classes and methods so as to meet all given requirements. 
Billing System for Shops 
Project Objective 
Create a console based C# (.NET) application that allows users to view shop item details and 
generate bills for customer purchases. 
The following are the tasks that need to be performed by the user: 
• View item details 
• Generate customer bill 
Overview 
View Item 
If the item ID is given, the item details should be returned. 
Generate Bill 
This function is used to generate a bill for items purchased in a shop. 
For the operation to be successful, the following conditions are to be met: 
• Item ID should be valid 
• Quantity purchased should be greater than zero 
If all these conditions are met, the bill amount should be calculated and an entry has to be made 
in the SHOP_BILL_TBL. 
A. Database Design 
1. Create a new user in database 
(To be done in the backend using SQL commands) 
a) Note: Do NOT use the default scott/tiger account of Oracle for this project. 
b) Username / password format: 
SHP<batchnumber><employeeid> 
Example: 
If batch number is 39806 and employee number is 12345, 
Username & password → SHP3980612345 
c) For database connection, use: 
• Service Name: orcl 
• Port Number: 1521 
2. Steps for creating a new user 
a) Open command prompt 
b) sqlplus / as sysdba 
c) create user <username> identified by <password>; 
d) grant connect, resource to <username>; 
e) commit; 
f) exit; 
3. Create Tables 
(To be done using SQL commands after logging in as the new user) 
Table Name: SHOP_ITEM_TBL 
Column Name Datatype 
Item_ID 
Description 
VARCHAR2(10) Primary Key 
Item_Name 
VARCHAR2(30) Item name 
Price 
NUMBER(8,2) Price per unit 
Sample Records 
Item_ID Item_Name Price 
S501 
Notebook 
S502 
40 
Pen 
10 
Table Name: SHOP_BILL_TBL 
Column Name Datatype 
Description 
Bill_ID 
NUMBER(4) 
Primary Key 
Item_ID 
VARCHAR2(10) Foreign Key, references Item_ID of SHOP_ITEM_TBL 
Quantity 
NUMBER(4) 
Quantity purchased 
Total_Amount NUMBER(10,2) Total bill amount 
Bill_Date 
DATE 
Date of billing 
B. System Design 
Namespace Structure 
Namespace 
Usage 
Com.Wipro.Shop.Service Displays console menu, validates input, invokes DAO operations 
Com.Wipro.Shop.Bean 
Contains entity class ShopBillBean 
Com.Wipro.Shop.Dao 
Contains database-related ADO.NET code 
Com.Wipro.Shop.Util 
Contains DB connection and user-defined exception 
Namespace: Com.Wipro.Shop.Util 
Class: DBUtil 
Method 
Description 
public static SqlConnection 
GetDBConnection() 
Establishes a database connection and returns 
SqlConnection reference 
Class: InvalidQuantityException 
Method 
Description 
public override string ToString() Returns string "INVALID QUANTITY" 
The details about when it has to be thrown is given in the appropriate methods. 
Namespace: Com.Wipro.Shop.Bean 
Class 
Method and 
Variables 
Description 
ShopBillBean 
Class 
private int billID 
Bill Id 
private string 
itemID 
Item billed 
Maps to Item_ID field of SHOP_BILL_TBL 
private int quantity 
private double 
totalAmount 
Quantity purchased 
Total bill amount 
private DateTime 
billDate 
Date of billing – current date [System.DateTime] 
Properties (Get/Set) Should create the public getter and setter properties for 
all the attributes mentioned in the class 
Namespace: Com.Wipro.Shop.Dao 
Class Method and Variables Description 
ShopDAO 
 
DAO class 
 
public int 
generateSequenceNumber() 
• This method generates a 4-digit auto
generated number 
 
public bool validateItem(string 
itemID) 
• Check SHOP_ITEM_TBL and return true 
if item ID is valid, else return false 
 
public double getItemPrice(string 
itemID) 
• Return price of the item 
 
public bool 
generateBill(ShopBillBean 
billBean) 
• Insert billBean values into 
SHOP_BILL_TBL 
• Bill_ID generated using 
generateSequenceNumber() 
• Bill date is today’s date 
(System.DateTime.Now) 
• Return true if insertion is successful 
 
Namespace: Com.Wipro.Shop.Service 
Class: ShopMain 
Class Method and Variables Description 
ShopMain 
 
Main class 
 
public static void 
Main(string[] args) 
The code that is needed to test your program goes here. 
 
public string 
viewItem(string 
itemID) 
Steps to perform: 
1. Validate itemID 
2. If valid, return item details 
3. If invalid return "INVALID ITEM ID" 
 
public string 
bill(ShopBillBean 
billBean) 
Steps to perform: 
1. If billBean is null return "INVALID" 
2. Validate itemID. If invalid return "INVALID ITEM 
ID" 
3. Validate quantity 
4. Throw InvalidQuantityException if quantity is less 
than or equal to zero and catch it in the same method. If 
caught return "INVALID QUANTITY" 
[Do NOT use Environment.Exit(0)] 
5. Calculate total amount using item price and quantity 
6. Generate bill using DAO 
7. If successful return "SUCCESS" 
Main Method (Correct Implementation) 
public static void Main(string[] args) 
{ 
ShopMain shopMain = new ShopMain(); 
// Test Case 1: View Item (Valid) 
Console.WriteLine(shopMain.viewItem("S501")); 
// Test Case 2: Generate Bill (Valid) 
ShopBillBean b1 = new ShopBillBean(); 
b1.ItemID = "S501"; 
b1.Quantity = 10; 
b1.BillDate = DateTime.Now; 
Console.WriteLine(shopMain.bill(b1)); 
// Test Case 3: Invalid Quantity 
ShopBillBean b2 = new ShopBillBean(); 
b2.ItemID = "S502"; 
b2.Quantity = 0; 
Console.WriteLine(shopMain.bill(b2)); 
// Test Case 4: Invalid Item ID 
Console.WriteLine(shopMain.viewItem("S999")); 
// Test Case 5: Null Bill 
Console.WriteLine(shopMain.bill(null)); 
} 
Sample Output 
Test Case 1: View Item 
ITEM DETAILS FOUND 
Test Case 2: Generate Bill (Valid) 
SUCCESS 
Test Case 3: Invalid Quantity 
INVALID QUANTITY 
Test Case 4: Invalid Item ID 
INVALID ITEM ID 
Test Case 5: Null Bill 
INVALID
