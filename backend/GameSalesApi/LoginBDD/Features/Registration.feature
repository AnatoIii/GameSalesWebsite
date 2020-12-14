Feature: Registration
	User enter the site and want to register new account

@RegistartionTests
Scenario: Register OK
	Given Launch Firefox
	And Navigate to Web Frontend
	Then click on Login button
	Then click on Register button
	When Enter valid email
	And Enter valid username
	And Enter "test" for first name
	And Enter "test" for last name
	And Enter "TestPass1234%" for password
	And Enter "TestPass1234%" for confirm password
	And Click on Register button
	Then Register sucesfull

Scenario: Register with email that exist in DB
	Given Launch Firefox
	And Navigate to Web Frontend
	Then click on Login button
	Then click on Register button
	When Enter existed "admin@admin.com" for email
	And Enter valid username
	And Enter "test" for first name
	And Enter "test" for last name
	And Enter "TestPass1234%" for password
	And Enter "TestPass1234%" for confirm password
	And Click on Register button
	Then message "User with email `admin@admin.com` already exist!" and be on register page

Scenario: Register with username that exist in DB
	Given Launch Firefox
	And Navigate to Web Frontend
	Then click on Login button
	Then click on Register button
	When Enter valid email
	And Enter existed "admin" for username
	And Enter "test" for first name
	And Enter "test" for last name
	And Enter "TestPass1234%" for password
	And Enter "TestPass1234%" for confirm password
	And Click on Register button
	Then message "User with username `admin` already exist!" and be on register page

Scenario: Register with incorrect password
	Given Launch Firefox
	And Navigate to Web Frontend
	Then click on Login button
	Then click on Register button
	When Enter valid email
	And Enter valid username
	And Enter "test" for first name
	And Enter "test" for last name
	And Enter "invalidPass" for password
	And Enter "invalidPass" for confirm password
	Then message under password "Password must contain uppercase, lowercase, digits."
