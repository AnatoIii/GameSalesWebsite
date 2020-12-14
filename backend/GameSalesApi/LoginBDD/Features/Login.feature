Feature: LogIn
	User enter the site and want to log in

@LoginTests
Scenario: LogIn with correct credentials
	Given LogIn Launch Firefox
	And LogIn Navigate to Web Frontend
	Then click on Login button for LogIn
	When Enter "admin@admin.com" for login 
	And "TestPass1234%" for password
	And Click on LogIn button
	Then login sucesfull

Scenario: LogIn with incorrect password
	Given LogIn Launch Firefox
	And LogIn Navigate to Web Frontend
	Then click on Login button for LogIn
	When Enter "admin@admin.com" for login 
	And "invalidPass" for password
	And Click on LogIn button
	Then message "Incorrect password"