# IdentityApp
## This is ASP.NET Core application with registration and authentication used basic CRUD operations.

Authenticated users should have access the user management table: id, name, e-mail, last login time, registration time, status (active/blocked). 
If user account is blocked or deleted any next user’s request should redirect to the login page. Blocked user can not be able to login, deleted user can re-register.

![image](https://user-images.githubusercontent.com/100798944/207672883-7d6ce1ec-15f8-4981-8c84-1bcf8cf65c89.png)

## The toolbar has 3 actions:
+ Block
+ Unblock
+ Detele

## You have to highlight the users using the checkboxes, then click on the action on the toolbar.

To run IdentityApp you need to change the connection string in the database in IdentityApp/appsettings.json

Link to deployed version [IdentityApp](http://akurgansky-001-site1.ctempurl.com/)
