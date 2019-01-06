![RestSharpLogo](https://github.com/RobBrowning/RestSharpExample/blob/master/RestSharpExample/ReadMe_Images/RestSharp_logo.png)
      
# RestSharpExample
This is an example using RestSharp

## Using OnTestAutomation.com examples
Using these examples helped me get started. Very easy to follow and clear to understand.

https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/

I will start to create my own examples and put these into a CI build.

To get public APIs to practice with I used this list - https://github.com/toddmotto/public-apis
Also I will use this site for Star Wars data for testing with https://swapi.co/documentation

### How to identify tests to automate API testing
The hardest part I find is identifying the tests to automate, I start with the below thoughts of typical API testing, 

Common Tests
* To verify whether the return value is based on input condition. Response of the APIs should be verified based on the request.
* To verify whether the system is authenticating the outcome when the API is updating any data structure
* To verify whether the API triggers some other event or request another API
* To verify the behaviour of the API when there is no return value

What needs to be verified in API testing
* Data accuracy
* HTTP status codes
* Response time
* Error codes in case API returns any errors
* Authorization checks
* Non functional testing such as performance testing, security testing

Reference - https://www.softwaretestingmaterial.com/api-testing/

Start to look at the GUI of the application if it has one, this can quickly give you ideas for tests. Write down parts of the application that could be tested and logic of the application, authentication, authorisation, validation, add, edit, delete, create users? After you have a good understanding of the GUI of the application and keeping in mind the common tests, start to look at the API documentation (if any).

Along with all this knowledge I will use Fiddler application as a proxy to verify requests and responses whilst using the GUI of the application. This is to gather examples of the API calls being used. This can help us better understand the API or spot API calls not documented. 

Also I can use Fiddler alongside Visual Studio and running tests to verify requests and responses.

![ChromeFiddler](https://github.com/RobBrowning/RestSharpExample/blob/master/RestSharpExample/ReadMe_Images/ChromeFiddler.PNG)
      
![FiddlerIcons](https://github.com/RobBrowning/RestSharpExample/blob/master/RestSharpExample/ReadMe_Images/FiddlerIcons.png)


### Builds and Releases

### Test examples
Using RestSharp NuGet package I installed on the project, I used the examples provided in the blog from - https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/ I carried on and used the same structure throughout as it was easy to read and understand the test and steps taken within.

Arrange - create the client and the request and the type of request i.e. Get method

Act - Execute the request created

Assert - Assert the expected outcome depending on the test.

![TestOne](https://github.com/RobBrowning/RestSharpExample/blob/master/RestSharpExample/ReadMe_Images/TestOne.PNG)


