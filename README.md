# Call Vlocity APIs from C#
## Setup
### Create a Connected App in your Salesforce Org
#### 1 - Setup => Create => Apps => Connected Apps => New
#### 2 - Enter a "Connected App Name" and a "Contact Email"
#### 3 - Select "Enable OAuth Settings"
#### 4 - "Callback URL": http://localhost
#### 5 - "Selected OAuth Scopes": Select them all and "Add"
#### 6 - Save. You should a message saying "Allow from 2-10 minutes for your changes to take effect on the server before using the connected app"
#### 7 - Click "Continue"
#### 8 - Copy the "Consumer Key" and "Consumer Secret" (click on "Click to reveal" for that one)
### Get a Security Token (Optional)
### Update the C# App
#### 1 - Open the App.config file
#### 2 - Change the "Security Token" (optional), "ConsumerKey", "ConsumerSecret", "Username", "Password" keys with your values
### Update the Program.cs file
#### 1 - The "GetOAuthToken" method is generic and will work for any Salesforce REST API call, standard or Vlocity
#### 2 - Create your own method to call the API you want to use, using the "CreateMoveOrder" method as an example. It calls an Integration Procedure but any Vlocity API can be called the same way. Refer to the Vlocity documentation.
